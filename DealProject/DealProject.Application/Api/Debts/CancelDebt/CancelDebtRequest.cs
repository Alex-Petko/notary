using DealProject.Domain;
using MediatR;

namespace DealProject.Application;

public sealed record CancelDebtRequest(CancelDebtDto Dto, string Login) 
    : DebtStatusRequest(Dto.DebtId, Login), IRequest<DealStatusType?>;
