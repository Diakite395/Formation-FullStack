using Api_TaskFlow_DotNet.Models;
using Api_TaskFlow_DotNet.Models.Dtos;

namespace Api_TaskFlow_DotNet.Repository.IRepository;

public interface IUserRepository
{
    User Create(CreateUser request);
    ICollection<User> GetUsers();
    User GetUser(Guid id);
    bool IsUniqueUser(string username, string email);
    bool ChangePassword(string password, Guid userId);
    User Update(User user);
    bool Delete(Guid id);
    bool SoftDelete(Guid id);
    User Login(LoginUser request);
}
