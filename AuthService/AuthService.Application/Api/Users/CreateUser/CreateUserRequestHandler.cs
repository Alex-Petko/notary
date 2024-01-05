using AuthService.Domain;
using AuthService.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Application;

internal sealed class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, IActionResult>
{
    private readonly ITransactions _transactions;
    private readonly IMapper _mapper;

    public CreateUserRequestHandler(ITransactions transactions, IMapper mapper)
    {
        _transactions = transactions;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.Dto);

        return await _transactions.TryCreateAsync(user) ? new OkResult() : new ConflictResult();
    }
}