using System.Diagnostics.CodeAnalysis;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
public record CreateUserDto(
    string Login,
    string PasswordHash
);
