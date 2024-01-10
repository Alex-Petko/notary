using AutoMapper;
using DebtManager.Domain;
using DebtManager.Infrastructure;

namespace DebtManager.Application;

internal sealed class BorrowDebtHandler : InitDebtHandler<BorrowDebtRequest>
{
    protected override DealStatusType DealStatus => DealStatusType.BorrowerApproved;

    public BorrowDebtHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    protected override string GetBorrowerLogin(BorrowDebtRequest request) => request.Sub;

    protected override string GetLenderLogin(BorrowDebtRequest request) => request.Body.Login;
}