using AccessControl.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Application;

internal sealed class CreateTokenHandler : IRequestHandler<CreateTokenRequest, IActionResult>
{
    private readonly ITokenManager _tokenManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public CreateTokenHandler(
        ITokenManager tokenManager,
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher)
    {
        _tokenManager = tokenManager;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<IActionResult> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FindAsync(request.Login);
        if (user is null)
            return new NotFoundResult();

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return new NotFoundResult();

        await _tokenManager.UpdateAsync(request.Login);

        return new OkResult();
    }

}