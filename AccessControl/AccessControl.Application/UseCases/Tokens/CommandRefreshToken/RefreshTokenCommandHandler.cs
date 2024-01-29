using AutoMapper;
using MediatR;

namespace AccessControl.Application;

internal sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand>
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

    public async Task Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var tokenManagerDto = _mapper.Map<TokenManagerDto>(command);
        await _tokenManager.UpdateAsync(tokenManagerDto, cancellationToken);
    }
}