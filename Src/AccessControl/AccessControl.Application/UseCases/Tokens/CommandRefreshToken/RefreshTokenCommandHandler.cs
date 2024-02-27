using AutoMapper;
using MediatR;

namespace AccessControl.Application;

internal sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResult>
{
    private readonly ITokenManager _tokenManager;
    private readonly IMapper _mapper;

    public RefreshTokenCommandHandler(
        ITokenManager tokenManager,
        IMapper mapper)
    {
        _tokenManager = tokenManager;
        _mapper = mapper;
    }

    public async Task<RefreshTokenCommandResult> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var tokenManagerDto = _mapper.Map<TokenManagerDto>(command);
        var result = await _tokenManager.RefreshAsync(tokenManagerDto, cancellationToken);

        if (result != RefreshResult.Ok)
            return RefreshTokenCommandResult.Fail;

        return RefreshTokenCommandResult.Ok;
    }
}