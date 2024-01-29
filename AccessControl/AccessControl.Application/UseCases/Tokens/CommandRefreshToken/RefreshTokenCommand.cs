using MediatR;
using NSwag.Annotations;
using Shared.Attributes;

namespace AccessControl.Application;

public sealed record RefreshTokenCommand : IRequest
{
    [FromSubClaim]
    [OpenApiIgnore]
    public string Login { get; init; } = null!;
}