using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class LendDebtCommandHandler : CreateDebtCommandHandler<LendDebtCommand>
{
    protected override DealStatusType DealStatus => DealStatusType.LenderApproved;

    public LendDebtCommandHandler(
        IQueryProvider queryProvider, 
        ICommandProvider commandProvider, 
        IMapper mapper) 
        : base(
            queryProvider, 
            commandProvider, 
            mapper)
    {
    }

    protected override string GetBorrowerLogin(LendDebtCommand command) => command.Body.Login;

    protected override string GetLenderLogin(LendDebtCommand command) => command.Login;
}