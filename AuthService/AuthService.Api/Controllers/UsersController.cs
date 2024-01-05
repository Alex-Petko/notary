using AuthService.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api;

[Route("[controller]")]
public sealed class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new CreateUserRequest(dto), cancellationToken);
    }
}