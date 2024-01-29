namespace Rent.Application;

public interface ICommandSetProvider<TEntity>
    where TEntity : class
{
    public void Add(TEntity entity);
}