using DealProject.Domain;
using DealProject.Infrastructure;

namespace DealProject.Application;

internal class DebtStatusHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public DebtStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected async Task<DealStatusType?> Handle(
        DebtStatusRequest request, 
        Func<DealStatusType, DealStatusType> lenderFunc, 
        Func<DealStatusType, DealStatusType> borrowerFunc)
    {
        var debt = await _unitOfWork.Debts.FindAsync(request.DebtId);
        if (debt == null)
            return null;

        var tempStatus = debt.Status;

        if (debt.LenderLogin == request.Login)
            debt.Status = lenderFunc(debt.Status);

        if (debt.BorrowerLogin == request.Login)
            debt.Status = borrowerFunc(debt.Status);

        if (tempStatus != debt.Status)
            await _unitOfWork.SaveChangesAsync();

        return debt.Status;
    }
}
