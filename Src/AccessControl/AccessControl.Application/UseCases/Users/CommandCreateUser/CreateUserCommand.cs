using MediatR;

namespace AccessControl.Application;

public sealed record CreateUserCommand : Credentials, IRequest<CreateUserCommandResult>;
