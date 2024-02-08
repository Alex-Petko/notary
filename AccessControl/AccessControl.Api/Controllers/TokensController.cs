using AccessControl.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api;

[Route("[controller]")]
public sealed class TokensController : ControllerBase
{
    private readonly IMediator _mediator;

    public TokensController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCommand([FromBody] CreateTokenCommand request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        if (result == CreateTokenCommandResult.AuthenticationFail)
            return NotFound();

        return Ok();
    }

    [HttpPost("refresh")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshCommand(RefreshTokenCommand request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);

        return result == RefreshTokenCommandResult.Ok ? Ok() : BadRequest();
    }
}