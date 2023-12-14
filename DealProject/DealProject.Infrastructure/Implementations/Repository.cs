using Shared.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Infrastructure;

[ExcludeFromCodeCoverage]
internal class Repository : BaseRepositoryHub, IRepository
{
    public IDebtRepository Debts { get; }

    public Repository(DealContext context) : base(context)
    {
        Debts = new DebtRepository(context);
    }
}
