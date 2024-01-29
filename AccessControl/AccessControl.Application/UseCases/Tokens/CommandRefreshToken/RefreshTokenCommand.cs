using MediatR;
using NSwag.Annotations;
using Shared.Attributes;

namespace AccessControl.Application;

public sealed record RefreshTokenCommand : IRequest<RefreshTokenCommandResult>
{
    [FromSubClaim]
    [OpenApiIgnore]
    public string Login { get; init; } = null!;
}
