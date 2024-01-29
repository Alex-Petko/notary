namespace AccessControl.Application;

internal interface ITokenManager
{
    Task CreateAsync(TokenManagerDto dto, CancellationToken cancellationToken = default);

    Task<RefreshResult> RefreshAsync(TokenManagerDto dto, CancellationToken cancellationToken = default);
}
