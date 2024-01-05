using System.Diagnostics.CodeAnalysis;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
public sealed class JwtOptions
{
    public virtual string Key { get; set; } = null!;
    public virtual int ExpiresMinutes { get; set; }
}