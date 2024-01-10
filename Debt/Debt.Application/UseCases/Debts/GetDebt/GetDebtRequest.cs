using MediatR;

namespace DebtManager.Application;

public sealed record GetDebtRequest(Guid Id) : IRequest<GetDebtDto?>;