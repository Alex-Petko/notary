using MediatR;
using Shared.Attributes;

namespace DebtManager.Application;

public sealed record GetDebtRequest : IRequest<GetDebtDto?>
{
    [SwaggerIgnore]
    public Guid DebtId { get; init; }
}