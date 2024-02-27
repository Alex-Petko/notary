using Rent.Domain;

namespace Rent.Application;

public interface ICommandProvider
{
    ICommandSetProvider<FileDescription> TemplateDescriptions { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
