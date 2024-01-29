namespace AccessControl.Domain;

public class JwtOptions
{
    public virtual Details JSONWebTokenDetails { get; set; } = null!;

    public virtual Details RefreshTokenDetails { get; set; } = null!;

    public class Details
    {
        public virtual string Key { get; set; } = null!;
        public virtual int ExpiresMinutes { get; set; }
    }
}