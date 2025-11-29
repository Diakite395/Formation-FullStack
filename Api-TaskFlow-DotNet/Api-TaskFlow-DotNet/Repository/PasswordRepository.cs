using System.Security.Cryptography;
using Api_TaskFlow_DotNet.Repository.IRepository;

namespace Api_TaskFlow_DotNet.Repository;

public class PasswordRepository : IPasswordRepository
{
    private const int SaltSize = 16;
    private const int HashSize = 32; // 32 bites
    // private const int HashSize = 512; // 512 bits. previously 32 bites
    private const int Iterations = 1000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool Verify(string password, string passwordHashed)
    {
        string[] parts = passwordHashed.Split("-");
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

        // return hash.SequenceEqual(inputHash); // Posible Timing Attack
        return CryptographicOperations.FixedTimeEquals(hash, inputHash);
    }
}