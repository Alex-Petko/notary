using AccessControl.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Api;

[Route("[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet($"{{{nameof(query)}.{nameof(Application.GetUserQuery.Login)}}}")]
    [ProducesResponseType(typeof(GetUserQuery), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetUserQuery(GetUserQuery query, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetUserQuery>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllUsersQuery(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUserCommand([FromBody] CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result == CreateUserCommandResult.Ok ? Ok() : Conflict();
    }
}