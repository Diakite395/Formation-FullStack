using System.Security.Cryptography;
using System.Text;
using Api_TaskFlow_DotNet.Data;
using Api_TaskFlow_DotNet.Models;
using Api_TaskFlow_DotNet.Models.Dtos;
using Api_TaskFlow_DotNet.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Api_TaskFlow_DotNet.Repository;

public class UserRepository(TaskFlowDbContext db,
                            IConfiguration config,
                            IPasswordRepository pwManager) : IUserRepository
{
    private readonly TaskFlowDbContext _db = db;
    private readonly IConfiguration _config = config;
    private readonly IPasswordRepository _password = pwManager;

    public bool ChangePassword(string password, Guid userId)
    {
        var user = _db.User.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            var passwordHash = _password.Hash(password);
            user.PasswordHash = passwordHash;
            _db.User.Update(user);
            _db.SaveChanges();
            return true;
        }
        
        return false;
    }

    public User Create(CreateUser request)
    {
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Role = "User",
            PasswordHash = _password.Hash(request.Password),
            CreatedAt = DateTime.UtcNow,
            State = 1
        };
        
        _db.User.Add(user);
        _db.SaveChanges();
        return _db.User.FirstOrDefault(u => u.Username == request.Username && u.Email == request.Email)!;
    }

    public bool Delete(Guid id)
    {
        var user = _db.User.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _db.User.Remove(user);
            _db.SaveChanges();
            return true;
        }
        
        return false;
    }

    public User GetUser(Guid id) =>
        _db.User.FirstOrDefault(u => u.Id == id)!;

    public ICollection<User> GetUsers() =>
        [.. _db.User];

    public bool IsUniqueUser(string username, string email)
    {
        var isUniqueUser = _db.User.FirstOrDefault(u => u.Username == username || u.Email == email);
        return isUniqueUser == null;
    }

    public User Login(LoginUser request)
    {
        var user = _db.User.FirstOrDefault(u => u.Email == request.Email);
        if (user == null)
        {
            return null!;
        }

        if (!_password.Verify(request.Password, user.PasswordHash))
        {
            return null!;
        }

        return user;
    }

    public bool SoftDelete(Guid id)
    {
        var user = _db.User.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            user.State = 0; 
            _db.User.Update(user);
            _db.SaveChanges();
            return true;
        }
        
        return false;
    }

    public User Update(User user)
    {
        _db.User.Update(user);
        _db.SaveChanges();
        return _db.User.First(u => u.Id == user.Id);
    }

    // public static string Obtenermd5(string password)
    // {
    //     byte[] data = Encoding.UTF8.GetBytes(password);
    //     data = MD5.HashData(data);

    //     string resp = "";
    //     for (int i = 0; i < data.Length; i++)
    //     {
    //         resp += data[i].ToString("x2").ToLower();
    //     }

    //     return resp;
    // }
}
