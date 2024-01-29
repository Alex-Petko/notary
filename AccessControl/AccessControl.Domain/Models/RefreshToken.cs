namespace AccessControl.Domain;

public sealed class RefreshToken
{
    public string Login { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public User User { get; set; } = null!;
}