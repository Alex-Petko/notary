using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Shared.Attributes;

namespace AccessControl.Application;

public sealed record RefreshTokenRequest : IRequest<IActionResult>
{
    [FromSubClaim]
    [OpenApiIgnore]
    public string Login { get; init; } = null!;
}