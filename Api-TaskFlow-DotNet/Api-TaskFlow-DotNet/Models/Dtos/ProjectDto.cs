using System;

namespace Api_TaskFlow_DotNet.Models.Dtos;

public sealed record ProjectResponse(
    Guid Id,
    string Name,
    string Description,
    DateTime CreateAt
);

public sealed record CreateProject(
    string Name,
    string Description
);

