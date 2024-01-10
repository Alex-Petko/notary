using AutoMapper;
using DebtManager.Domain;
using DebtManager.Infrastructure;

namespace DebtManager.Application;

internal sealed class LendDebtsHandler : InitDebtHandler<LendDebtRequest>
{
    protected override DealStatusType DealStatus => DealStatusType.LenderApproved;

    public LendDebtsHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }


    protected override string GetBorrowerLogin(LendDebtRequest request) => request.Login;

    protected override string GetLenderLogin(LendDebtRequest request) => request.Sub;
}
