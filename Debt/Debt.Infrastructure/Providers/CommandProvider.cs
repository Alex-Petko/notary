using DebtManager.Application;
using DebtManager.Domain;

namespace DebtManager.Infrastructure;

internal class CommandProvider : ICommandProvider
{
    private readonly Context _context;

    public ICommandSetProvider<Debt> Debts { get; }

    public CommandProvider(Context context)
    {
        _context = context;
        Debts = new CommandSetProvider<Debt>(context.Debts);
    }

    public Task AddDebtAsync(Debt debt)
    {
        _context.Debts.Add(debt);
        return _context.SaveChangesAsync();
    }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
