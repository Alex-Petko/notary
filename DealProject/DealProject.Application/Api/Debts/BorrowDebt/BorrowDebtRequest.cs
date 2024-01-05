using MediatR;

namespace DealProject.Application;

public sealed record BorrowDebtRequest(BorrowDebtDto Dto, string Login) 
    : IRequest<Guid>;
