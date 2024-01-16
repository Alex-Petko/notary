using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shared.Attributes;

namespace DebtManager.Application;

public record InitDebtRequest : IRequest<Guid>
{
    [FromBody]
    public InitDebtRequestBody Body { get; set; } = null!;

    [FromSubClaim, OpenApiIgnore]
    public string Login { get; set; }
}

public sealed record InitDebtRequestBody(
    string Login,
    int Sum,
    DateTime? Begin,
    DateTime? End = null);