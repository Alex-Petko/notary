using DealProject.Domain;
using MediatR;

namespace DealProject.Application;

public sealed record CloseDebtRequest(CloseDebtDto Dto, string Login) 
    : DebtStatusRequest(Dto.DebtId, Login), IRequest<DealStatusType?>;
