using AutoMapper;
using DebtManager.Domain;
using DebtManager.Infrastructure;

namespace DebtManager.Application;

internal sealed class LendDebtHandler : InitDebtHandler<LendDebtRequest>
{
    protected override DealStatusType DealStatus => DealStatusType.LenderApproved;

    public LendDebtHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    protected override string GetBorrowerLogin(LendDebtRequest request) => request.Body.Login;

    protected override string GetLenderLogin(LendDebtRequest request) => request?.Sub ?? throw new NotImplementedException();
}