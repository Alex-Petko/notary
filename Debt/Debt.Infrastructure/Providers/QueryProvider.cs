using AutoMapper;
using DebtManager.Application;
using DebtManager.Domain;

namespace DebtManager.Infrastructure;

internal class QueryProvider : Application.IQueryProvider
{
    public IQuerySetProvider<Debt> Debts { get; }

    public QueryProvider(Context context, IMapper mapper)
    {
        Debts = new QuerySetProvider<Debt>(context.Debts, mapper);
    }
}
