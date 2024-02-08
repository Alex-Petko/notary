using MediatR;

namespace AccessControl.Application;

internal sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<GetUserQueryResult>>
{
    private readonly IQueryProvider _queryProvider;

    public GetAllUsersQueryHandler(IQueryProvider queryProvider)
    {
        _queryProvider = queryProvider;
    }

    public Task<IEnumerable<GetUserQueryResult>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        return _queryProvider.Users.GetAllAsync<GetUserQueryResult>();
    }
}
