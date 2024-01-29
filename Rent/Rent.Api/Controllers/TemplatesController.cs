using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rent.Application;

namespace Rent.Api;

[Route("[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public TemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }

    [HttpGet("{query.Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Stream>> GetTemplateQuery(GetTemplateQuery query, CancellationToken cancellationToken)
    {
        var stream = await _mediator.Send(query, cancellationToken);
        if (stream == null)
            return NotFound();

        return File(stream, "application/msword");

    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddTemplateCommand(IFormFile formFile, CancellationToken cancellationToken)
    {
        var command = new AddTemplateCommand(new FormFileProxy(formFile));
        var result = await _mediator.Send(command, cancellationToken);

        return result ? Created() : Conflict();
    }
}
