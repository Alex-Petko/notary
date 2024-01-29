using Rent.Domain;

namespace Rent.Application;

public interface IQueryProvider
{
    IQuerySetProvider<FileDescription> TemplateDescriptions { get; }
}
