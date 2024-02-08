
namespace AccessControl.Client;

public sealed class UsersClientProxy : UsersClient
{
    public UsersClientProxy(string baseUrl, HttpClient httpClient) : base(baseUrl, httpClient)
    {
    }

    public override async Task<ICollection<GetUserQuery>> GetAllUsersQueryAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.GetAllUsersQueryAsync(cancellationToken);
        }
        catch (ApiException e)
        {
            if (e.StatusCode == 204)
                return null!;

            throw;
        }
    }

    public override async Task<GetUserQuery> GetUserQueryAsync(string query_Login, CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.GetUserQueryAsync(query_Login, cancellationToken);
        }
        catch (ApiException e)
        {
            if (e.StatusCode == 204)
                return null!;

            throw;
        }
    }
}
