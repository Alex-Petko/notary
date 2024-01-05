using MediatR;

namespace DealProject.Application;

public sealed record LendDebtRequest(LendDebtDto Dto, string Login) : IRequest<Guid>;
