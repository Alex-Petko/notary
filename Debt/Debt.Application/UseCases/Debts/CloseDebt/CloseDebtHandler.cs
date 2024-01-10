using DebtManager.Domain;
using DebtManager.Infrastructure;

namespace DebtManager.Application;

internal sealed class CloseDebtHandler : DebtStatusHandler<CloseDebtRequest>
{
    public CloseDebtHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override DealStatusType LenderHandler(DealStatusType status) => status switch
    {
        DealStatusType.Opened => DealStatusType.LenderClosed,
        DealStatusType.BorrowerClosed => DealStatusType.Closed,
        _ => status,
    };

    protected override DealStatusType BorrowerHandler(DealStatusType status) => status switch
    {
        DealStatusType.Opened => DealStatusType.BorrowerClosed,
        DealStatusType.LenderClosed => DealStatusType.Closed,
        _ => status,
    };
}