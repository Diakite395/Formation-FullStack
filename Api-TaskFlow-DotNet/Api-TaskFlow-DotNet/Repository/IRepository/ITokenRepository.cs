using Api_TaskFlow_DotNet.Models.Dtos;

namespace Api_TaskFlow_DotNet.Repository.IRepository;

public interface ITokenRepository
{
    TokenResponse GenerateTokens(string username, string role);
    string GenerateAccessToken(string username, string role);
    string GenerateRefreshToken();
    TokenResponse Refresh(string refreshtoken);
}
