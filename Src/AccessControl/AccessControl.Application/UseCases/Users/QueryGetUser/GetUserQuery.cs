using MediatR;
using NSwag.Annotations;

namespace AccessControl.Application;

public sealed record GetUserQuery : IRequest<GetUserQueryResult>
{
    [OpenApiIgnore]
    public string Login { get; init; } = null!;
}
