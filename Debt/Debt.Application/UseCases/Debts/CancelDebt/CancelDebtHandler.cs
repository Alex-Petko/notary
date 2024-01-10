using DebtManager.Domain;
using DebtManager.Infrastructure;
using MediatR;

namespace DebtManager.Application;

internal sealed class CancelDebtHandler : DebtStatusHandler<CancelDebtRequest>
{
    public CancelDebtHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override DealStatusType LenderHandler(DealStatusType status) => status switch
    {
        DealStatusType.LenderApproved => DealStatusType.LenderCanceled,
        DealStatusType.BorrowerApproved => DealStatusType.LenderCanceled,
        DealStatusType.BorrowerCanceled => DealStatusType.Canceled,
        _ => status,
    };

    protected override DealStatusType BorrowerHandler(DealStatusType status) => status switch
    {
        DealStatusType.LenderApproved => DealStatusType.BorrowerCanceled,
        DealStatusType.BorrowerApproved => DealStatusType.BorrowerCanceled,
        DealStatusType.LenderCanceled => DealStatusType.Canceled,
        _ => status,
    };
}
