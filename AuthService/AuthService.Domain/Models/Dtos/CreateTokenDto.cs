using System.Diagnostics.CodeAnalysis;

namespace AuthService.Domain;

[ExcludeFromCodeCoverage]
public record CreateTokenDto(
    string Login,
    string PasswordHash
);
