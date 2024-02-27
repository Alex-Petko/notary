using AccessControl.Domain;

namespace AccessControl.Application;

public interface IQueryProvider
{
    IQuerySetProvider<User> Users { get; }
}
