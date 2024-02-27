using MediatR;

namespace AccessControl.Application;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<GetUserQueryResult>>;
