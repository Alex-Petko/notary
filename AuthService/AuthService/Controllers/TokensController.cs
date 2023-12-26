using AuthService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[Route("[controller]")]
public sealed class TokensController : ControllerBase
{
    private readonly IMediator _mediator;

    public TokensController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateTokenDto dto, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateTokenRequest(dto, Response.Cookies), cancellationToken);
        return result;
    }
}