using MediatR;
using Shared.Attributes;

namespace DebtManager.Application;

public sealed record GetDebtRequest([SwaggerIgnore] Guid Id) : IRequest<GetDebtDto?>;