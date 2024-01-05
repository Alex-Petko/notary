using DealProject.Domain;
using DealProject.Infrastructure;

namespace DealProject.Application;

internal class InitDebtHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public InitDebtHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected async Task<Guid> Handle(
        Debt debt,
        string lender,
        string borrower,
        DealStatusType status)
    {
        debt.LenderLogin = lender;
        debt.BorrowerLogin = borrower;
        debt.Status = status;

        _unitOfWork.Debts.Add(debt);

        await _unitOfWork.SaveChangesAsync();

        return debt.Id;
    }
}
