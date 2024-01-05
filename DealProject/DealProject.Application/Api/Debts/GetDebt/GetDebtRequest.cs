using MediatR;

namespace DealProject.Application;

public sealed record GetDebtRequest(Guid Id) : IRequest<GetDebtDto?>;
