using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;

namespace AccessControl.Application;

public sealed record RefreshTokenRequest : IRequest<IActionResult>
{
    [FromSubClaim]
    public string Login { get; init; } = null!;
}
