using DebtManager.Domain;
using MediatR;

namespace DebtManager.Application;

internal abstract class ChangeDebtStatusCommandHandler<TRequest> : IRequestHandler<TRequest, DealStatusType?>
    where TRequest : ChangeDebtStatusCommand
{
    private readonly ICommandProvider _commandProvider;

    public ChangeDebtStatusCommandHandler(ICommandProvider commandProvider)
    {
        _commandProvider = commandProvider;
    }

    public async Task<DealStatusType?> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var debt = await _commandProvider.Debts.FindAsync(request.DebtId);
        if (debt == null)
            return null;

        var tempStatus = debt.Status;

        if (debt.LenderLogin == request.Login)
            debt.Status = LenderHandler(debt.Status);

        if (debt.BorrowerLogin == request.Login)
            debt.Status = BorrowerHandler(debt.Status);

        if (tempStatus != debt.Status)
            await _commandProvider.SaveChangesAsync();

        return debt.Status;
    }

    protected abstract DealStatusType LenderHandler(DealStatusType status);

    protected abstract DealStatusType BorrowerHandler(DealStatusType status);
}