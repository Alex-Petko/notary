namespace Rent.Api;

internal sealed class WebHostEnvironmentProxy : Application.IWebHostEnvironment
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public string WebRootPath
    {
        get => _webHostEnvironment.WebRootPath;
        set => _webHostEnvironment.WebRootPath = value;
    }

    public WebHostEnvironmentProxy(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
}
