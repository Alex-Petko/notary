using DealProject.Domain;
using Shared.Repositories;

namespace DealProject.Infrastructure;

public interface IDebtRepository : IRepository<Debt, Guid>
{
}