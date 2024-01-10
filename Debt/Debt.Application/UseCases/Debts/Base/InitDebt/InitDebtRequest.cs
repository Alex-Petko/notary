using MediatR;
using Shared.Attributes;

namespace DebtManager.Application;

public record InitDebtRequest : IRequest<Guid>
{
    public string Login { get; init; } = null!;

    public int Sum { get; init; }

    public DateTime? Begin { get; init; }

    public DateTime? End { get; init; }

    [FromSubClaim]
    public string Sub { get; init; } = null!;
}