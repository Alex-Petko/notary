using MediatR;

namespace DealProject.Application;

public sealed record GetAllDebtsRequest() : IRequest<IEnumerable<GetDebtDto>>;
