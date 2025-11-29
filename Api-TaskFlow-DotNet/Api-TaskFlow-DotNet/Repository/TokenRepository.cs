using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api_TaskFlow_DotNet.Models.Dtos;
using Api_TaskFlow_DotNet.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Api_TaskFlow_DotNet.Data;
using Api_TaskFlow_DotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_TaskFlow_DotNet.Repository;

public class TokenRepository : ITokenRepository
{
    private readonly TaskFlowDbContext _db;
    private readonly IConfiguration _config;
    private readonly string _secretKey;
    private readonly string Issuer ; // Issuer : qui a émis le token
    private readonly string Audience; // Audience : pour qui le token est destiné
    private readonly int ExpirationInMinutes;
    private readonly int RefreshTokenExpirationInDays;
    // private readonly Dictionary<string, string> _refreshToken = new Dictionary<string, string>();

    public TokenRepository(IConfiguration config, TaskFlowDbContext db)
    {
        _db = db;
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _secretKey = _config.GetValue<string>("JwtConfig:Secret") ?? "default_fallback_secret: TaskFlow. Learn Resfull API With TaskFlowApi Project.";

        Issuer = _config.GetValue<string>("JwtConfig:Issuer") ?? "https://Myapi.com";
        Audience = _config.GetValue<string>("JwtConfig:Audience") ?? "https://MyapiUsers.com";
        ExpirationInMinutes = _config.GetValue<int>("JwtConfig:ExpirationInMinutes", 1);
        RefreshTokenExpirationInDays = _config.GetValue<int>("JwtConfig:RefreshTokenExpirationInDays", 7);
    }

    public string GenerateAccessToken(string username, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(ExpirationInMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    public TokenResponse GenerateTokens(string username, string role)
    {
        var token = _db.Token.FirstOrDefault(t => t.User.Username == username);
         
        // Remove existing refresh token

        if(token != null)
        {
            _db.Token.Remove(token!);
            _db.SaveChanges();
        }
        
        // Generate new Access Token
        var accessToken = GenerateAccessToken(username, role);
        var refreshToken = GenerateRefreshToken();

        // Save refresh token in database
        var newToken = new Token
        {
            RefreshToken = refreshToken,
            User = _db.User.First(u => u.Username == username),
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(RefreshTokenExpirationInDays)
        };

        _db.Token.Add(newToken);
        _db.SaveChanges();

        return _db.Token.First(t => t.RefreshToken == refreshToken).ToTokenResponse(accessToken);
    }

    public TokenResponse Refresh(string refreshtoken)
    {
        var token = _db.Token
            .Include(t => t.User) // Without this, t.User will be null
            .FirstOrDefault(t => t.RefreshToken == refreshtoken);

        if (token != null && token.RefreshTokenExpiryTime > DateTime.UtcNow)
        {
            var username = token.User.Username;
            var role = token.User.Role;

            // Generate new Access Token
            var accessToken = GenerateAccessToken(username, role);

            return new TokenResponse(
                AccessToken: accessToken,
                RefreshToken: refreshtoken
            );
        }
        
        if(token != null && token.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            _db.Token.Remove(token!);
            _db.SaveChanges();
            return null!;
        }

        return null!;
    }
}
