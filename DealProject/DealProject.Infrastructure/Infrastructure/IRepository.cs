using Shared.Repositories;

namespace DealProject.Infrastructure;

public interface IRepository : IBaseRepositoryHub
{
    IDebtRepository Debts { get; }
}