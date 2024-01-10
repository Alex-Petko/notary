using AutoMapper;
using DebtManager.Domain;
using DebtManager.Infrastructure;
using MediatR;

namespace DebtManager.Application;

internal abstract class InitDebtHandler<TRequest> : IRequestHandler<TRequest, Guid>
    where TRequest : InitDebtRequest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    protected abstract DealStatusType DealStatus { get; }

    public InitDebtHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var debt = _mapper.Map<Debt>(request);

        debt.BorrowerLogin = GetBorrowerLogin(request);
        debt.LenderLogin = GetLenderLogin(request);
        debt.Status = DealStatus;

        _unitOfWork.Debts.Add(debt);

        await _unitOfWork.SaveChangesAsync();

        return debt.Id;
    }

    protected abstract string GetBorrowerLogin(TRequest request);

    protected abstract string GetLenderLogin(TRequest request);
}
