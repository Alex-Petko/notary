namespace Rent.Domain;

public sealed class FileDescription
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Path { get; set; } = null!;
    
    public FileDescription(string name, string path)
    {
        Name = name;
        Path = path;
    }
}
