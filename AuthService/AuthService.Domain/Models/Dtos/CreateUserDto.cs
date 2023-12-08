using System.Diagnostics.CodeAnalysis;

namespace AuthService.Domain;

[ExcludeFromCodeCoverage]
public record CreateUserDto(
    string Login,
    string PasswordHash
);
