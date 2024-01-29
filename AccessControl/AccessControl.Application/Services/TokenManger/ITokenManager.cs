namespace AccessControl.Application;

internal interface ITokenManager
{
    Task UpdateAsync(TokenManagerDto dto, CancellationToken cancellationToken = default);
}