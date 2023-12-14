using System.Diagnostics.CodeAnalysis;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
public record CreateTokenDto(
    string Login,
    string PasswordHash
);
