using AutoMapper;
using MediatR;

namespace DebtManager.Application;

internal sealed class GetDebtQueryHandler : IRequestHandler<GetDebtQuery, GetDebtQueryResult?>
{
    private readonly IQueryProvider _queryProvider;
    private readonly IMapper _mapper;

    public GetDebtQueryHandler(IQueryProvider queryProvider, IMapper mapper)
    {
        _queryProvider = queryProvider;
        _mapper = mapper;
    }

    public async Task<GetDebtQueryResult?> Handle(GetDebtQuery query, CancellationToken cancellationToken)
    {
        var debt = await _queryProvider.Debts.FindAsync(query.DebtId, cancellationToken);
        var result = debt is not null ? _mapper.Map<GetDebtQueryResult>(debt) : null;
        return result;
    }
}