using System.Diagnostics.CodeAnalysis;

namespace AuthService.Domain;

[ExcludeFromCodeCoverage]
public class User
{
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}