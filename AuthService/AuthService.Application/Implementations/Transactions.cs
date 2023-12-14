using AuthService.Domain;
using AuthService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
internal class Transactions : ITransactions
{
    private readonly IRepository _repository;
    private readonly ILogger<Transactions> _logger;

    public Transactions(IRepository repository, ILogger<Transactions> logger)
    {
        _repository = repository;
        _logger = logger;
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
        try
        {
            await using var transaction = await _repository.BeginTransactionAsync();

            var dbUser = await _repository.Users.FindAsync(user.Login);
            if (dbUser != null)
                return false;

            _repository.Users.Add(user);
            await _repository.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (NpgsqlException e)
        {
            _logger.LogError(e, e.Message);
            return false;
        }

        return true;
    }
}
