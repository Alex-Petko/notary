namespace DebtManager.Application;

public sealed record AcceptDebtRequest(Guid DebtId) : DebtStatusRequest(DebtId);