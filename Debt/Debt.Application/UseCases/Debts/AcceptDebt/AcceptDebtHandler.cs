using DebtManager.Domain;
using DebtManager.Infrastructure;
using MediatR;

namespace DebtManager.Application;

internal sealed class AcceptDebtHandler : DebtStatusHandler<AcceptDebtRequest>
{
    public AcceptDebtHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override DealStatusType LenderHandler(DealStatusType status) => status switch
    {
        DealStatusType.BorrowerApproved => DealStatusType.Opened,
        _ => status,
    };

    protected override DealStatusType BorrowerHandler(DealStatusType status) => status switch
    {
        DealStatusType.LenderApproved => DealStatusType.Opened,
        _ => status,
    };
}
