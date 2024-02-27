using MediatR;

namespace Rent.Application;

internal sealed class GetTemplateQueryHandler : IRequestHandler<GetTemplateQuery, Stream?>
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IQueryProvider _queryProvider;

    public GetTemplateQueryHandler(IWebHostEnvironment webHostEnvironment, IQueryProvider queryProvider)
    {
        _webHostEnvironment = webHostEnvironment;
        _queryProvider = queryProvider;
    }

    public async Task<Stream?> Handle(GetTemplateQuery query, CancellationToken cancellationToken)
    {
        var templateDescription = await _queryProvider.TemplateDescriptions.FindAsync(query.Id);
        if (templateDescription == null)
            return null;

        var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, templateDescription.Path);
        return new FileStream(fullPath, FileMode.Open);
    }
}
