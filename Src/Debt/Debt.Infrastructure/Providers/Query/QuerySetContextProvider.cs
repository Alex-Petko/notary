using AutoMapper;
using AutoMapper.QueryableExtensions;
using DebtManager.Application;
using Microsoft.EntityFrameworkCore;

namespace DebtManager.Infrastructure;

internal sealed class QuerySetContextProvider<TEntity> : IQuerySetProvider<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly IMapper _mapper;

    public QuerySetContextProvider(DbSet<TEntity> dbSet, IMapper mapper)
    {
        _dbSet = dbSet;
        _mapper = mapper;
    }

    public Task<TEntity?> FindAsync(
        object?[] keyValues,
        CancellationToken cancellationToken = default)
        => _dbSet.FindAsync(keyValues, cancellationToken).AsTask();

    public Task<TEntity?> FindAsync(
        object keyValue, 
        CancellationToken cancellationToken = default)
        => _dbSet.FindAsync(new[] { keyValue }, cancellationToken).AsTask();


    public async Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken cancellationToken = default) 
        => await _dbSet
            .AsNoTracking()
            .ProjectTo<T>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
}
