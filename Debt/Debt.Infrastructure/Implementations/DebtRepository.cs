using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace DebtManager.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class DebtRepository : RepositoryBase<Domain.Debt, Guid>, IDebtRepository
{
    public DebtRepository(DealContext context) : base(context)
    {
    }
}