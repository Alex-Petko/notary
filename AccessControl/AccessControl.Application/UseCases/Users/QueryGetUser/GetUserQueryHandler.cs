using AutoMapper;
using MediatR;

namespace AccessControl.Application;

internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResult>
{
    private readonly IQueryProvider _queryProvider;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IQueryProvider queryProvider, IMapper mapper)
    {
        _queryProvider = queryProvider;
        _mapper = mapper;
    }

    public async Task<GetUserQueryResult> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await _queryProvider.Users.FindAsync(query.Login);
        var result = _mapper.Map<GetUserQueryResult>(user);

        return result;
    }
}
