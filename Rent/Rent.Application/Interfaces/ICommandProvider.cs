using Rent.Domain;

namespace Rent.Application;

public interface ICommandProvider
{
    public ICommandSetProvider<FileDescription> TemplateDescriptions { get; }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
