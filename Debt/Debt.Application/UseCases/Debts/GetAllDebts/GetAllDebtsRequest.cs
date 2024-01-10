using MediatR;

namespace DebtManager.Application;

public sealed record GetAllDebtsRequest() : IRequest<IEnumerable<GetDebtDto>>;
