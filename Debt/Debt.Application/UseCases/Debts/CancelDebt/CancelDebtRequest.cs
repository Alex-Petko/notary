namespace DebtManager.Application;

public sealed record CancelDebtRequest(Guid DebtId) : DebtStatusRequest(DebtId);
