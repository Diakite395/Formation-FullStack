namespace Api_TaskFlow_DotNet.Models.Dtos;

// Un record en C# est un type spécial introduit pour faciliter la création 
// d’objets immuables (des objets dont les valeurs ne changent pas après création).
public sealed record CreateUser(
    string Username,
    string Email,
    string Password
);

public sealed record LoginUser(
    string Email,
    string Password
);

public sealed record UpdateUser(
    Guid Id,
    string Username,
    string Email,
    string Role
);

public sealed record UserResponse(
    Guid Id,
    string Username,
    string Email,
    string Role,
    int State,
    DateTime CreatedAt,
    TokenResponse Token
);
public sealed record UsersResponse(
    Guid Id,
    string Username,
    string Email,
    string Role,
    DateTime CreatedAt
);
