using System.Diagnostics.CodeAnalysis;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
public sealed record CreateUserDto(
    string Login,
    string PasswordHash
);