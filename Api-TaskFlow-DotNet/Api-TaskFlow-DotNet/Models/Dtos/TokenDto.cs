using System;

namespace Api_TaskFlow_DotNet.Models.Dtos;

public sealed record TokenResponse(
    string AccessToken,
    string RefreshToken
);
