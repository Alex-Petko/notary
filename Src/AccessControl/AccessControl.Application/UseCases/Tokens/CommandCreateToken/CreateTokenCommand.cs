using MediatR;

namespace AccessControl.Application;

public sealed record CreateTokenCommand : Credentials, IRequest<CreateTokenCommandResult>;