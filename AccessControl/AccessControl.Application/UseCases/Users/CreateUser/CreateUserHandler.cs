using AccessControl.Domain;
using AccessControl.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Application;

internal sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, IActionResult>
{
    private readonly ITransactions _transactions;
    private readonly IMapper _mapper;

    public CreateUserHandler(ITransactions transactions, IMapper mapper)
    {
        _transactions = transactions;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        return await _transactions.TryCreateAsync(user) ? new OkResult() : new ConflictResult();
    }
}