using AccessControl.Domain;
using AutoMapper;
using MediatR;

namespace AccessControl.Application;

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResult>
{
    private readonly ICommandProvider _commandProvider;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(
        ICommandProvider commandProvider,
        IMapper mapper,
        IPasswordHasher passwordHasher)
    {
        _commandProvider = commandProvider;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateUserCommandResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(command);
        user.PasswordHash = _passwordHasher.HashPassword(user, command.Password);

        var result = await _commandProvider.TryCreateUserAsync(user, cancellationToken);

        return result ? CreateUserCommandResult.Ok : CreateUserCommandResult.CreateUserFail;
    }
}