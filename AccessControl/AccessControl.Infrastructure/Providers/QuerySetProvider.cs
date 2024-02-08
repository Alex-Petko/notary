using AccessControl.Application;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AccessControl.Infrastructure;

internal class QuerySetProvider<TEntity> : IQuerySetProvider<TEntity>
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;
    private readonly IMapper _mapper;

    public QuerySetProvider(DbSet<TEntity> dbSet, IMapper mapper)
    {
        _dbSet = dbSet;
        _mapper = mapper;
    }

    public Task<TEntity?> FindAsync(params object?[]? keyValues) => _dbSet.FindAsync(keyValues).AsTask();

    public async Task<IEnumerable<T>> GetAllAsync<T>(CancellationToken cancellationToken = default)
    {
        return await _dbSet
          .AsNoTracking()
          .ProjectTo<T>(_mapper.ConfigurationProvider)
          .ToListAsync(cancellationToken);
    }
}