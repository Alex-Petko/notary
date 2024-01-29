namespace AccessControl.Application;

public interface ICommandSetProvider<TEntity>
    where TEntity : class
{
    void Add(TEntity entity);
}