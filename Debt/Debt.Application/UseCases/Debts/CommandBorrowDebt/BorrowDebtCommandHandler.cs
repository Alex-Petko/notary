using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class BorrowDebtCommandHandler : CreateDebtCommandHandler<BorrowDebtCommand>
{
    protected override DealStatusType DealStatus => DealStatusType.BorrowerApproved;

    public BorrowDebtCommandHandler(ICommandProvider commandProvider, IMapper mapper) : base(commandProvider, mapper)
    {
    }

    protected override string GetBorrowerLogin(BorrowDebtCommand request) => request.Login;

    protected override string GetLenderLogin(BorrowDebtCommand request) => request.Body.Login;
}