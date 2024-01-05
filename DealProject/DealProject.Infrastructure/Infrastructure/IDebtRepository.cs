using DealProject.Domain;
using Shared.Repositories;

namespace DealProject.Infrastructure;

public interface IDebtRepository : IRepositoryBase<Debt, Guid>
{
}