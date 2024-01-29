using Microsoft.AspNetCore.Identity;

namespace AccessControl.Application;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IQueryProvider _queryProvider;
    private readonly IPasswordHasher _passwordHasher;

    public AuthenticationService(
        IQueryProvider queryProvider,
        IPasswordHasher passwordHasher)
    {
        _queryProvider = queryProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResult> AuthenticateAsync(AuthenticationDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _queryProvider.Users.FindAsync(dto.Login);
        if (user is null)
            return AuthenticationResult.UserNotFound;

        cancellationToken.ThrowIfCancellationRequested();

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
            return AuthenticationResult.PasswordIncorrect;

        return AuthenticationResult.Ok;
    }
}
