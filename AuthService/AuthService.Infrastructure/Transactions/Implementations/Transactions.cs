using AuthService.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class Transactions : ITransactions
{
    private readonly IRepository _repository;

    public Transactions(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ContainsAsync(User user)
    {
        var userFromDb = await _repository
            .Users
            .GetAsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Login == user.Login &&
                x.PasswordHash == user.PasswordHash);

        return userFromDb != null;
    }

    public async Task<bool> TryCreateAsync(User user)
    {
        await using var transaction = await _repository.BeginTransactionAsync(IsolationLevel.RepeatableRead);

        var dbUser = await _repository.Users.FindAsync(user.Login);
        if (dbUser != null)
            return false;

        _repository.Users.Add(user);
        await _repository.SaveChangesAsync();

        await transaction.CommitAsync();

        return true;
    }
}
