using DealProject.Domain;
using MediatR;

namespace DealProject.Application;

public sealed record AcceptDebtRequest(AcceptDebtDto Dto, string Login) 
    : DebtStatusRequest(Dto.DebtId, Login), IRequest<DealStatusType?>;
