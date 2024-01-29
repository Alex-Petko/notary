using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class CloseDebtCommandHandler : ChangeDebtStatusCommandHandler<CloseDebtCommand>
{
    public CloseDebtCommandHandler(ICommandProvider commandProvider) : base(commandProvider)
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