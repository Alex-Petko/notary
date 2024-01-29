using DebtManager.Application;
using DebtManager.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebtManager.Api;

[Authorize]
[ApiController]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
    [ProducesResponseType(typeof(IEnumerable<GetDebtDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var debts = await _mediator.Send(new GetAllDebtsRequest(), cancellationToken);
        return Ok(debts);
    }

    [HttpGet("{" + $"{nameof(request)}.{nameof(GetDebtRequest.DebtId)}" + "}")]
    [ResponseCache(Duration = 30)]
    [ProducesResponseType(typeof(GetDebtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(GetDebtRequest request, CancellationToken cancellationToken)
    {
        var debt = await _mediator.Send(request, cancellationToken);
        return debt is not null ? Ok(debt) : NotFound();
    }

    [HttpPost("lend")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Lend(LendDebtRequest request, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(request, cancellationToken);
        return Created("", id);
    }

    [HttpPost("borrow")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Borrow(BorrowDebtRequest request, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(request, cancellationToken);
        return Created("", id);
    }

    [HttpPost("accept")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Accept(AcceptDebtRequest request, CancellationToken cancellationToken)
    {
        var status = await _mediator.Send(request, cancellationToken);
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("cancel")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cancel(CancelDebtRequest request, CancellationToken cancellationToken)
    {
        var status = await _mediator.Send(request, cancellationToken);
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("close")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Close(CloseDebtRequest request, CancellationToken cancellationToken)
    {
        var status = await _mediator.Send(request, cancellationToken);
        return status is not null ? Ok(status) : BadRequest();
    }
}