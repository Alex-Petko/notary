using MediatR;
using NSwag.Annotations;

namespace DebtManager.Application;

public sealed record GetDebtRequest : IRequest<GetDebtDto?>
{
    [OpenApiIgnore]
    public Guid DebtId { get; init; }
}