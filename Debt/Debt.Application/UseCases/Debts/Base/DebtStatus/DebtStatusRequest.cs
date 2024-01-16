using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shared.Attributes;

namespace DebtManager.Application;

public record class DebtStatusRequest : IRequest<DealStatusType?>
{
    [FromBody]
    public Guid DebtId { get; init; }

    [FromSubClaim, OpenApiIgnore]
    public string Login { get; init; } = null!;
}