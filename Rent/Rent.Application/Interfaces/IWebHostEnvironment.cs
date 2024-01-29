namespace Rent.Application;

public interface IWebHostEnvironment
{
    //
    // Сводка:
    //     Gets or sets the absolute path to the directory that contains the web-servable
    //     application content files. This defaults to the 'wwwroot' subfolder.
    string WebRootPath { get; set; }
}