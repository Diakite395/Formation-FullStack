namespace Api_TaskFlow_DotNet.Repository.IRepository;

public interface IPasswordRepository
{
    string Hash(string password);
    bool Verify(string password, string passwordHashed);
}

