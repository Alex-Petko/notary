using DealProject.Domain;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Infrastructure;

[ExcludeFromCodeCoverage]
internal class DebtRepository : Repository<Debt, Guid>, IDebtRepository
{
    public DebtRepository(DealContext context) : base(context)
    {
    }
}