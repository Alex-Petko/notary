using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class AcceptDebtCommandHandler : ChangeDebtStatusCommandHandler<AcceptDebtCommand>
{
    public AcceptDebtCommandHandler(ICommandProvider commandProvider) : base(commandProvider)
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