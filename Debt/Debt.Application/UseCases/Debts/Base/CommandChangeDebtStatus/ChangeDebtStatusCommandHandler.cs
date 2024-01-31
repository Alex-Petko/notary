using DebtManager.Domain;
using MediatR;

namespace DebtManager.Application;

internal abstract class ChangeDebtStatusCommandHandler<TCommand> : IRequestHandler<TCommand, DealStatusType?>
    where TCommand : ChangeDebtStatusCommand
{
    private readonly ICommandProvider _commandProvider;

    public ChangeDebtStatusCommandHandler(ICommandProvider commandProvider)
    {
        _commandProvider = commandProvider;
    }

    public async Task<DealStatusType?> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var debt = await _commandProvider.Debts.FindAsync(command.DebtId);
        if (debt == null)
            return null;

        var tempStatus = debt.Status;

        if (debt.LenderLogin == command.Login)
            debt.Status = LenderHandler(debt.Status);

        if (debt.BorrowerLogin == command.Login)
            debt.Status = BorrowerHandler(debt.Status);

        if (tempStatus != debt.Status)
            await _commandProvider.SaveChangesAsync();

        return debt.Status;
    }

    protected abstract DealStatusType LenderHandler(DealStatusType status);

    protected abstract DealStatusType BorrowerHandler(DealStatusType status);
}