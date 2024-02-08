using DebtManager.Application;
using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebtManager.Api;

[Authorize]
[ApiController]
[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
[Route("[controller]")]
public class DebtsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DebtsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ResponseCache(Duration = 30)]
    [ProducesResponseType(typeof(IEnumerable<GetDebtQueryResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllDebtsQuery(CancellationToken cancellationToken)
    {
        var debts = await _mediator.Send(new GetAllDebtsQuery(), cancellationToken);
        return Ok(debts);
    }

    [HttpGet($"{{{nameof(query)}.{nameof(Application.GetDebtQuery.DebtId)}}}")]
    [ResponseCache(Duration = 30)]
    [ProducesResponseType(typeof(GetDebtQueryResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDebtQuery(GetDebtQuery query, CancellationToken cancellationToken)
    {
        var debt = await _mediator.Send(query, cancellationToken);
        return debt is not null ? Ok(debt) : NotFound();
    }

    [HttpPost("lend")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> LendDebtCommand(LendDebtCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        if (id is null)
            return NotFound();

        return Created("", id);
    }

    [HttpPost("borrow")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> BorrowDebtCommand(BorrowDebtCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);
        if (id is null)
            return NotFound();

        return Created("", id);
    }

    [HttpPost("accept")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AcceptDebtCommand(AcceptDebtCommand command, CancellationToken cancellationToken)
    {
        var status = await _mediator.Send(command, cancellationToken);
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("cancel")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CancelDebtCommand(CancelDebtCommand command, CancellationToken cancellationToken)
    {
        var status = await _mediator.Send(command, cancellationToken);
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("close")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CloseDebtCommand(CloseDebtCommand command, CancellationToken cancellationToken)
    {
        var status = await _mediator.Send(command, cancellationToken);
        return status is not null ? Ok(status) : BadRequest();
    }
}