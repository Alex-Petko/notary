using AutoMapper;
using DebtManager.Domain;
using MediatR;

namespace DebtManager.Application;

internal abstract class CreateDebtCommandHandler<TCommand> : IRequestHandler<TCommand, Guid>
    where TCommand : CreateDebtCommand
{
    private readonly ICommandProvider _commandProvider;
    private readonly IMapper _mapper;

    protected abstract DealStatusType DealStatus { get; }

    public CreateDebtCommandHandler(ICommandProvider commandProvider, IMapper mapper)
    {
        _commandProvider = commandProvider;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(TCommand command, CancellationToken cancellationToken)
    {
        var debt = _mapper.Map<Debt>(command);

        debt.BorrowerLogin = GetBorrowerLogin(command);
        debt.LenderLogin = GetLenderLogin(command);
        debt.Status = DealStatus;

        await _commandProvider.AddDebtAsync(debt);

        return debt.Id;
    }

    protected abstract string GetBorrowerLogin(TCommand request);

    protected abstract string GetLenderLogin(TCommand request);
}