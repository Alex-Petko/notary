using DebtManager.Domain;
using Shared.Repositories;

namespace DebtManager.Infrastructure;

public interface IDebtRepository : IRepositoryBase<Domain.Debt, Guid>
{
}