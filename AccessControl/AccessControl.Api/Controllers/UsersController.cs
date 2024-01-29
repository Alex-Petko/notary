﻿using AccessControl.Application;
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

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCommand([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result == CreateUserCommandResult.Ok ? Ok() : Conflict();
    }
}