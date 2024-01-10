using AutoMapper;
using AutoMapper.QueryableExtensions;
using DebtManager.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Application;

internal sealed class GetAllDebtsHandler : IRequestHandler<GetAllDebtsRequest, IEnumerable<GetDebtDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllDebtsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetDebtDto>> Handle(GetAllDebtsRequest request, CancellationToken cancellationToken)
    {
        return await _unitOfWork
            .Debts
            .GetAsNoTracking()
            .ProjectTo<GetDebtDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}