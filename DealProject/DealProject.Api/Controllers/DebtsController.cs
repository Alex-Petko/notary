using DealProject.Application;
using DealProject.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Attributes;

namespace DealProject.Api;

[Authorize]
[ApiController]
public class DebtsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DebtsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{controller}")]
    [ProducesResponseType(typeof(IEnumerable<GetDebtDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var debts = await _mediator.Send(new GetAllDebtsRequest());
        return Ok(debts);
    }

    [HttpGet("{controller}/{id}")]
    [ProducesResponseType(typeof(GetDebtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromQuery] Guid id)
    {
        var debt = await _mediator.Send(new GetDebtRequest(id));
        return debt is not null ? Ok(debt) : NotFound();
    }

    [HttpPost("{controller}/lend")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Lend([FromBody] LendDebtDto dto, [FromSubClaim] string login)
    {
        var id = await _mediator.Send(new LendDebtRequest(dto, login));
        return Created("", id);
    }

    [HttpPost("{controller}/borrow")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Borrow([FromBody] BorrowDebtDto dto, [FromSubClaim] string login)
    {
        var id = await _mediator.Send(new BorrowDebtRequest(dto, login));
        return Created("", id);
    }

    [HttpPost("{controller}/accept")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Accept([FromBody] AcceptDebtDto dto, [FromSubClaim] string login)
    {
        var status = await _mediator.Send(new AcceptDebtRequest(dto, login));
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("{controller}/cancel")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cancel([FromBody] CancelDebtDto dto, [FromSubClaim] string login)
    {
        var status = await _mediator.Send(new CancelDebtRequest(dto, login));
        return status is not null ? Ok(status) : BadRequest();
    }

    [HttpPost("{controller}/close")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Close([FromBody] CloseDebtDto dto, [FromSubClaim] string login)
    {
        var status = await _mediator.Send(new CloseDebtRequest(dto, login));
        return status is not null ? Ok(status) : BadRequest();
    }
}