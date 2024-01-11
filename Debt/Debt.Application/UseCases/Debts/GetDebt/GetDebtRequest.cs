using MediatR;

namespace DebtManager.Application;

public sealed record GetDebtRequest(Guid DebtId) : IRequest<GetDebtDto?>;