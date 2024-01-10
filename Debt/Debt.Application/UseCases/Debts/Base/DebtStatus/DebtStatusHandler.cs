using DebtManager.Domain;
using DebtManager.Infrastructure;
using MediatR;

namespace DebtManager.Application;

internal abstract class DebtStatusHandler<TRequest> : IRequestHandler<TRequest, DealStatusType?>
    where TRequest : DebtStatusRequest
{
    private readonly IUnitOfWork _unitOfWork;

    public DebtStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DealStatusType?> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var debt = await _unitOfWork.Debts.FindAsync(request.DebtId);
        if (debt == null)
            return null;

        var tempStatus = debt.Status;

        if (debt.LenderLogin == request.Login)
            debt.Status = LenderHandler(debt.Status);

        if (debt.BorrowerLogin == request.Login)
            debt.Status = BorrowerHandler(debt.Status);

        if (tempStatus != debt.Status)
            await _unitOfWork.SaveChangesAsync();

        return debt.Status;
    }

    protected abstract DealStatusType LenderHandler(DealStatusType status);

    protected abstract DealStatusType BorrowerHandler(DealStatusType status);
}