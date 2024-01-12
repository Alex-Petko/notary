using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Application;

[ExcludeFromCodeCoverage]
public class JwtOptions
{
    public virtual Details Jwt { get; set; } = null!;

    public virtual Details Rt { get; set; } = null!;

    public class Details
    {
        public virtual string Key { get; set; } = null!;
        public virtual int ExpiresMinutes { get; set; }
    }
}