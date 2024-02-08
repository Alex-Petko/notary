using AccessControl.Application;
using AccessControl.Domain;
using AutoMapper;

namespace AccessControl.Infrastructure;

internal class QueryProvider : Application.IQueryProvider
{
    public IQuerySetProvider<User> Users { get; }

    public QueryProvider(Context context, IMapper mapper)
    {
        Users = new QuerySetProvider<User>(context.Users, mapper);
    }
}
