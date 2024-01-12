using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Application;

internal sealed class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, IActionResult>
{
    private readonly ITokenManager _tokenManager;

    public RefreshTokenHandler(ITokenManager tokenManager)
    {
        _tokenManager = tokenManager;
    }

    public async Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        await _tokenManager.UpdateAsync(request.Login);

        return new OkResult();
    }
}