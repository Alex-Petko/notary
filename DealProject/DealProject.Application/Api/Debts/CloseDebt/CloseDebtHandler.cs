using DealProject.Domain;
using DealProject.Infrastructure;
using MediatR;

namespace DealProject.Application;

internal sealed class CloseDebtHandler : DebtStatusHandler, IRequestHandler<CloseDebtRequest, DealStatusType?>
{
    public CloseDebtHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Task<DealStatusType?> Handle(CloseDebtRequest request, CancellationToken cancellationToken)
    {
        return Handle(
            request,
            LenderHandler,
            BorrowerHandler);
    }

    private DealStatusType LenderHandler(DealStatusType status) => status switch
    {
        DealStatusType.Opened => DealStatusType.LenderClosed,
        DealStatusType.BorrowerClosed => DealStatusType.Closed,
        _ => status,
    };

    private DealStatusType BorrowerHandler(DealStatusType status) => status switch
    {
        DealStatusType.Opened => DealStatusType.BorrowerClosed,
        DealStatusType.LenderClosed => DealStatusType.Closed,
        _ => status,
    };
}