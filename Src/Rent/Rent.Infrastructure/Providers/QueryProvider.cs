using Rent.Application;
using Rent.Domain;

namespace Rent.Infrastructure;

internal class QueryProvider : Application.IQueryProvider
{
    public IQuerySetProvider<FileDescription> TemplateDescriptions { get; }

    public QueryProvider(Context context)
    {
        TemplateDescriptions = new QuerySetProvider<FileDescription>(context.TemplateDescriptions);
    }
}
