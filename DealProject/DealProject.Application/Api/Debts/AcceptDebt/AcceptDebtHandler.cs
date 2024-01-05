using DealProject.Domain;
using DealProject.Infrastructure;
using MediatR;

namespace DealProject.Application;

internal sealed class AcceptDebtHandler : DebtStatusHandler, IRequestHandler<AcceptDebtRequest, DealStatusType?>
{
    public AcceptDebtHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public Task<DealStatusType?> Handle(AcceptDebtRequest request, CancellationToken cancellationToken)
    {
        return Handle(
            request,
            LenderHandler,
            BorrowerHandler);
    }

    private DealStatusType LenderHandler(DealStatusType status) => status switch
    {
        DealStatusType.BorrowerApproved => DealStatusType.Opened,
        _ => status,
    };

    private DealStatusType BorrowerHandler(DealStatusType status) => status switch
    {
        DealStatusType.LenderApproved => DealStatusType.Opened,
        _ => status,
    };
}
