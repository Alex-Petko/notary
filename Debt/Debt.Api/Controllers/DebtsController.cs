﻿using DebtManager.Application;
using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebtManager.Api;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DebtsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DebtsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<GetDebtDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var debts = await _mediator.Send(new GetAllDebtsRequest());
        return Ok(debts);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetDebtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] GetDebtRequest request)
    {
        var debt = await _mediator.Send(request);
        return debt is not null ? Ok(debt) : NotFound();
    }

    [HttpPost("lend")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Lend(LendDebtRequest request)
    {
        var id = await _mediator.Send(request);
        return Created("", id);
    }

    [HttpPost("borrow")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Borrow([FromBody] BorrowDebtRequest request)
    {
        var id = await _mediator.Send(request);
        return Created("", id);
    }

    [HttpPost("accept")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Accept([FromBody] AcceptDebtRequest request)
    {
        var status = await _mediator.Send(request);
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("cancel")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cancel([FromBody] CancelDebtRequest request)
    {
        var status = await _mediator.Send(request);
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("close")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Close([FromBody] CloseDebtRequest request)
    {
        var status = await _mediator.Send(request);
        return status is not null ? Ok(status) : BadRequest();
    }
}