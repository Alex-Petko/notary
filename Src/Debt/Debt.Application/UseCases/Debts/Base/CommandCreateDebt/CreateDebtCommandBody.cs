namespace DebtManager.Application;

public sealed record CreateDebtCommandBody(
    string Login,
    int Sum,
    DateTimeOffset? Begin,
    DateTimeOffset? End = null);