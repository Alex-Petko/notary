using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shared.Attributes;

namespace DebtManager.Application;

public record CreateDebtCommand : IRequest<Guid?>
{
    [FromBody]
    public CreateDebtCommandBody Body { get; set; } = null!;

    [FromSubClaim]
    [OpenApiIgnore]
    public string Login { get; set; } = null!;
}
