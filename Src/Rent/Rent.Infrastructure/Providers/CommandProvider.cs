using Rent.Application;
using Rent.Domain;

namespace Rent.Infrastructure;

internal class CommandProvider : ICommandProvider
{
    private readonly Context _context;

    public ICommandSetProvider<FileDescription> TemplateDescriptions { get; }

    public CommandProvider(Context context)
    {
        _context = context;
        TemplateDescriptions = new CommandSetProvider<FileDescription>(context.TemplateDescriptions);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) 
        => _context.SaveChangesAsync(cancellationToken);
}
