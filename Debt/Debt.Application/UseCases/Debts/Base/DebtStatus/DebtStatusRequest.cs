using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;

namespace DebtManager.Application;

public record class DebtStatusRequest : IRequest<DealStatusType?>
{
    [FromBody]
    public Guid DebtId { get; init; }

    [FromSubClaim]
    public string Login { get; init; } = null!;
}