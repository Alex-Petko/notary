using DealProject.Domain;
using DealProject.Infrastructure;
using MediatR;

namespace DealProject.Application;

internal sealed class CancelDebtHandler : DebtStatusHandler, IRequestHandler<CancelDebtRequest, DealStatusType?>
{
    public CancelDebtHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Task<DealStatusType?> Handle(CancelDebtRequest request, CancellationToken cancellationToken)
    {
        return Handle(
            request,
            LenderHandler,
            BorrowerHandler);
    }

    private DealStatusType LenderHandler(DealStatusType status) => status switch
    {
        DealStatusType.LenderApproved => DealStatusType.LenderCanceled,
        DealStatusType.BorrowerApproved => DealStatusType.LenderCanceled,
        DealStatusType.BorrowerCanceled => DealStatusType.Canceled,
        _ => status,
    };

    private DealStatusType BorrowerHandler(DealStatusType status) => status switch
    {
        DealStatusType.LenderApproved => DealStatusType.BorrowerCanceled,
        DealStatusType.BorrowerApproved => DealStatusType.BorrowerCanceled,
        DealStatusType.LenderCanceled => DealStatusType.Canceled,
        _ => status,
    };
}
