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

    public async Task<GetDebtQueryResult?> Handle(GetDebtQuery request, CancellationToken cancellationToken)
    {
        var entity = await _queryProvider.Debts.FindAsync(request.DebtId);
        return entity is not null ? _mapper.Map<GetDebtQueryResult>(entity) : null;
    }
}