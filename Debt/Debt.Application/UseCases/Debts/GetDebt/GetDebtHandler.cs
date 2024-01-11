﻿using AutoMapper;
using DebtManager.Infrastructure;
using MediatR;

namespace DebtManager.Application;

internal sealed class GetDebtHandler : IRequestHandler<GetDebtRequest, GetDebtDto?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDebtHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetDebtDto?> Handle(GetDebtRequest request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Debts.FindAsync(request.DebtId);
        return entity is not null ? _mapper.Map<GetDebtDto>(entity) : null;
    }
}