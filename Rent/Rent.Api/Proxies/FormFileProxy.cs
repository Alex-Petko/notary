using Rent.Application;

namespace Rent.Api;

internal sealed class FormFileProxy : IFile
{
    private readonly IFormFile _formFile;

    public string Name => _formFile.Name;

    public string FileName => _formFile.FileName;

    public string ContentType => _formFile.ContentType;

    public long Length => _formFile.Length;

    public FormFileProxy(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        return _formFile.CopyToAsync(target, cancellationToken);
    }
}
