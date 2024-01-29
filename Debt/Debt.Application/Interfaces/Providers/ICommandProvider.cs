using DebtManager.Domain;

namespace DebtManager.Application;

public interface ICommandProvider
{
    ICommandSetProvider<Debt> Debts { get; }

    Task AddDebtAsync(Debt debt);

    Task SaveChangesAsync();
}
