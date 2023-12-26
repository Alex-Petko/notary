using System.Diagnostics.CodeAnalysis;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
public sealed record CreateTokenDto(
    string Login,
    string PasswordHash);