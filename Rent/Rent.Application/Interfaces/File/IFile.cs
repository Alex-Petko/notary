namespace Rent.Application;

public interface IFile
{
    string Name { get; }

    string FileName { get; }

    string ContentType { get; }

    long Length { get; }

    Task CopyToAsync(Stream target, CancellationToken cancellationToken = default);
}
