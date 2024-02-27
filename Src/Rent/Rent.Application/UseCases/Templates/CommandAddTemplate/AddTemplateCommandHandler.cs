using MediatR;
using Rent.Domain;

namespace Rent.Application;

internal sealed class AddTemplateCommandHandler : IRequestHandler<AddTemplateCommand, bool>
{
    private const string DirectoryOfTemplates = "Templates";

    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ICommandProvider _commandProvider;

    public AddTemplateCommandHandler(IWebHostEnvironment webHostEnvironment, ICommandProvider commandProvider)
    {
        _webHostEnvironment = webHostEnvironment;
        _commandProvider = commandProvider;
    }

    public async Task<bool> Handle(AddTemplateCommand command, CancellationToken cancellationToken)
    {
        var directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, DirectoryOfTemplates);
        Directory.CreateDirectory(directoryPath);

        var fullPath = Path.Combine(directoryPath, command.FileStream.FileName);
        if (File.Exists(fullPath))
            return false;
        
        using (var stream = File.Create(fullPath))
        {
            await command.FileStream.CopyToAsync(stream);
        }

        var path = Path.Combine(DirectoryOfTemplates, command.FileStream.FileName);
        var fileDescription = new FileDescription(command.FileStream.FileName, path);

        _commandProvider.TemplateDescriptions.Add(fileDescription);

        await _commandProvider.SaveChangesAsync(cancellationToken);
        return true;
    }
}
