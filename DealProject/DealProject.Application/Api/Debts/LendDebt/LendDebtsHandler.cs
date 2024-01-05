using AutoMapper;
using DealProject.Domain;
using DealProject.Infrastructure;
using MediatR;

namespace DealProject.Application;

internal sealed class LendDebtsHandler : InitDebtHandler, IRequestHandler<LendDebtRequest, Guid>
{
    private readonly IMapper _mapper;

    public LendDebtsHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        _mapper = mapper;
    }

    public Task<Guid> Handle(LendDebtRequest request, CancellationToken cancellationToken)
    {
        var debt = _mapper.Map<Debt>(request.Dto);

        return Handle(
            debt,
            request.Login,
            request.Dto.Login,
            DealStatusType.LenderApproved);
    }
}
