using System;

namespace Api_TaskFlow_DotNet.Models;

public class Token
{
    public Guid Id { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public User User { get; set; } = null!;
    public DateTime RefreshTokenExpiryTime { get; set; }
}
