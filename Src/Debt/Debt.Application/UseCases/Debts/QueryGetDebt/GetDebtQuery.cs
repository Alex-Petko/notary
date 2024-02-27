using MediatR;
using NSwag.Annotations;

namespace DebtManager.Application;

public sealed record GetDebtQuery : IRequest<GetDebtQueryResult?>
{
    [OpenApiIgnore]
    public Guid DebtId { get; init; }
}