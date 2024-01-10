namespace DebtManager.Application;

public sealed class JwtOptions
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
}