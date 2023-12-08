namespace AuthService;

public class JwtOptions
{
    public string Key { get; set; } = null!;
    public int ExpiresMinutes { get; set; }
}
