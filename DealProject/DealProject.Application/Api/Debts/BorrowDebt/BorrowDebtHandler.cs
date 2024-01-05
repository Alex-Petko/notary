using AutoMapper;
using DealProject.Domain;
using DealProject.Infrastructure;
using MediatR;

namespace DealProject.Application;

internal sealed class BorrowDebtHandler : InitDebtHandler, IRequestHandler<BorrowDebtRequest, Guid>
{
    private readonly IMapper _mapper;

    public BorrowDebtHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork) 
    {
        _mapper = mapper;
    }

    public Task<Guid> Handle(BorrowDebtRequest request, CancellationToken cancellationToken)
    {
        var debt = _mapper.Map<Debt>(request.Dto);

        return Handle(
            debt,
            request.Dto.Login,
            request.Login,
            DealStatusType.BorrowerApproved);
    }
}
