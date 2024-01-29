﻿using AccessControl.Application;
using AccessControl.Domain;

namespace AccessControl.Infrastructure;

internal class QueryProvider : Application.IQueryProvider
{
    public IQuerySetProvider<User> Users { get; }

    public QueryProvider(Context context)
    {
        Users = new QuerySetProvider<User>(context.Users);
    }
}
