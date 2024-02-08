namespace DebtManager.Domain;

public sealed class PostgresOptions
{
    public sealed class ConnectionStringsOptions
    {
        public string Default { get; init; } = null!;
    }

    public ConnectionStringsOptions ConnectionStrings { get; init; } = null!;
}