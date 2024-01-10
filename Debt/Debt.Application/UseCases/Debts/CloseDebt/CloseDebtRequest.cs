namespace DebtManager.Application;

public sealed record CloseDebtRequest(Guid DebtId) : DebtStatusRequest(DebtId);