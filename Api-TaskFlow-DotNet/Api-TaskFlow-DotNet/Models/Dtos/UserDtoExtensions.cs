namespace Api_TaskFlow_DotNet.Models.Dtos;

public static class UserDtoExtensions
{
    // Ce this devant le premier paramètre transforme la méthode en méthode d’extension.

    // Cela signifie que la méthode se comporte comme si elle faisait partie de la classe 
    // User, même si tu ne l’as pas écrite dans la classe User.
    // Tu peux donc l'appeler comme ceci : user.ToUserResponse();
    public static UserResponse ToUserResponse(this User user, TokenResponse token) =>
        new(
            user.Id,
            user.Username,
            user.Email,
            user.Role,
            user.State,
            user.CreatedAt,
            token
        );
    
    public static UsersResponse ToUsersResponse(this User user) =>
        new(
            user.Id,
            user.Username,
            user.Email,
            user.Role,
            user.CreatedAt
        );

    public static User ToUser(this UpdateUser updateUser, DateTime createdAt, string PasswordHash) =>
        new()
        {
            Id = updateUser.Id,
            Username = updateUser.Username,
            Email = updateUser.Email,
            Role = "User",
            PasswordHash = PasswordHash,
            CreatedAt = createdAt
        };
}
