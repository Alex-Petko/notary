using AutoMapper;
using MediatR;

namespace AccessControl.Application;

internal sealed class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, CreateTokenCommandResult>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ITokenManager _tokenManager;
    private readonly IMapper _mapper;

    public CreateTokenCommandHandler(
        IAuthenticationService authenticationService,
        ITokenManager tokenManager,
        IMapper mapper)
    {
        _authenticationService = authenticationService;
        _tokenManager = tokenManager;
        _mapper = mapper;
    }

    public async Task<CreateTokenCommandResult> Handle(CreateTokenCommand command, CancellationToken cancellationToken)
    {
        var authenticationDto = _mapper.Map<AuthenticationDto>(command);
        var authenticationResult = await _authenticationService.AuthenticateAsync(authenticationDto, cancellationToken);
        if (authenticationResult != AuthenticationResult.Ok)
            return CreateTokenCommandResult.AuthenticationFail;

        var tokenManagerDto = _mapper.Map<TokenManagerDto>(command);
        await _tokenManager.CreateAsync(tokenManagerDto, cancellationToken);

        return CreateTokenCommandResult.Ok;
    }
}