using AccessControl.Client;
using AutoMapper;
using DebtManager.Application;
using DebtManager.Domain;

namespace DebtManager.Infrastructure;

internal class QueryProvider : Application.IQueryProvider
{
    public IQuerySetProvider<Debt> Debts { get; }

    public IQueryUsersProvider Users { get; }

    public QueryProvider(
        Context context,
        IUsersClient usersClient,
        IMapper mapper,
        ICache cache)
    {
        Debts = new QuerySetContextProvider<Debt>(context.Debts, mapper);
        Users = new QueryUsersProvider(usersClient, cache, mapper);
    }
}
