using DebtManager.Domain;

namespace DebtManager.Application;

public interface IQueryProvider
{
    IQuerySetProvider<Debt> Debts { get; }
    IQueryUsersProvider Users { get; }
}
