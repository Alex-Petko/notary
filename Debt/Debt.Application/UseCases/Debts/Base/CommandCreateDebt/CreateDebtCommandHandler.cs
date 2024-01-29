using AutoMapper;
using DebtManager.Domain;
using MediatR;

namespace DebtManager.Application;

internal abstract class CreateDebtCommandHandler<TRequest> : IRequestHandler<TRequest, Guid>
    where TRequest : CreateDebtCommand
{
    private readonly ICommandProvider _commandProvider;
    private readonly IMapper _mapper;

    protected abstract DealStatusType DealStatus { get; }

    public CreateDebtCommandHandler(ICommandProvider commandProvider, IMapper mapper)
    {
        _commandProvider = commandProvider;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var debt = _mapper.Map<Debt>(request);

        debt.BorrowerLogin = GetBorrowerLogin(request);
        debt.LenderLogin = GetLenderLogin(request);
        debt.Status = DealStatus;

        await _commandProvider.AddDebtAsync(debt);

        return debt.Id;
    }

    protected abstract string GetBorrowerLogin(TRequest request);

    protected abstract string GetLenderLogin(TRequest request);
}