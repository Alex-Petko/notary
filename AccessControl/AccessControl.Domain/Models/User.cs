namespace AccessControl.Domain;

public sealed class User
{
    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public RefreshToken? RefreshToken { get; set; }
}