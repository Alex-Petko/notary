using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;

namespace DebtManager.Application;

public record class DebtStatusRequest([FromBody] Guid DebtId) : IRequest<DealStatusType?>
{
    [FromSubClaim]
    public string Login { get; init; } = null!;
}