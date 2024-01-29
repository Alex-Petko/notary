using MediatR;

namespace DebtManager.Application;

public sealed record GetAllDebtsQuery : IRequest<IEnumerable<GetDebtQueryResult>>;