using DealProject.Domain;
using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class DebtRepository : RepositoryBase<Debt, Guid>, IDebtRepository
{
    public DebtRepository(DealContext context) : base(context)
    {
    }
}