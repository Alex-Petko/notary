using AuthService.Domain;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AuthService.Application;

internal class UserCreator : IUserCreator
{
    private readonly ITransactions _transactions;
    private readonly IMapper _mapper;

    public UserCreator(ITransactions transactions, IMapper mapper)
    {
        _transactions = transactions;
        _mapper = mapper;
    }

    public async Task<bool> ExecuteAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        return await _transactions.TryCreateAsync(user);
    }
}
