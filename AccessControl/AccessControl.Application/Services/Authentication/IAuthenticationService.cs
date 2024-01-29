
namespace AccessControl.Application;

internal interface IAuthenticationService
{
    Task<AuthenticationResult> AuthenticateAsync(AuthenticationDto dto, CancellationToken cancellationToken = default);
}
