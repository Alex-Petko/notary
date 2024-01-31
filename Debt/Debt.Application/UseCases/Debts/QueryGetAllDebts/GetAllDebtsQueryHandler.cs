using MediatR;

namespace DebtManager.Application;

internal sealed class GetAllDebtsQueryHandler : IRequestHandler<GetAllDebtsQuery, IEnumerable<GetDebtQueryResult>>
{
    private readonly IQueryProvider _queryProvider;

    public GetAllDebtsQueryHandler(IQueryProvider queryProvider)
    {
        _queryProvider = queryProvider;
    }

    public Task<IEnumerable<GetDebtQueryResult>> Handle(GetAllDebtsQuery query, CancellationToken cancellationToken)
    {
        return _queryProvider.Debts.GetAllAsync<GetDebtQueryResult>(cancellationToken);
    }
}