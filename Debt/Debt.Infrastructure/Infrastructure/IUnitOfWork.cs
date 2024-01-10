using Shared.Repositories;

namespace DebtManager.Infrastructure;

public interface IUnitOfWork : IUnitOfWorkBase
{
    IDebtRepository Debts { get; }
}