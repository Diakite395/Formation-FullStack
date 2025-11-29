using System;

namespace Api_TaskFlow_DotNet.Models.Dtos;

public static class TokenDtoExtensions
{
    public static TokenResponse ToTokenResponse(this Token token, string accessToken) =>
        new(
            AccessToken: accessToken,
            RefreshToken: token.RefreshToken
        );
}
