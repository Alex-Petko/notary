namespace DebtManager.Application;

public sealed record CreateDebtCommandBody(
    string Login,
    int Sum,
    DateTime? Begin,
    DateTime? End = null);