namespace DebtManager.Domain;

public sealed class RedisOptions
{
    public string ConnectionString { get; init; } = null!;
}
