using Shared.Repositories;

namespace DealProject.Infrastructure;

public interface IUnitOfWork : IUnitOfWorkBase
{
    IDebtRepository Debts { get; }
}