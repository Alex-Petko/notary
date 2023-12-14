﻿using DealProject.Application;
using DealProject.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DealProject;

[Authorize]
[ApiController]
public class DebtsController : ControllerBase
{
    private readonly IDebtService _service;

    public DebtsController(IDebtService service)
    {
        _service = service;
    }

    [HttpGet("{controller}")]
    [ProducesResponseType(typeof(IEnumerable<GetDebtDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var debts = await _service.GetAllAsync();
        return Ok(debts);
    }

    [HttpGet("{controller}/{id}")]
    [ProducesResponseType(typeof(GetDebtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var debt = await _service.GetAsync(id);
        return debt != null ? Ok(debt) : NotFound();
    }

    [HttpPost("{controller}/lend")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Lend(LendDebtDto dto)
    {
        var login = GetLogin();
        var id = await _service.LendAsync(login, dto);

        return Created("", id);
    }

    [HttpPost("{controller}/borrow")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Borrow(BorrowDebtDto dto)
    {
        var login = GetLogin();
        var id = await _service.BorrowAsync(login, dto);

        return Created("", id);
    }

    [HttpPost("{controller}/accept")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Accept(AcceptDebtDto dto)
    {
        var login = GetLogin();
        var status = await _service.AcceptAsync(login, dto);

        return status != null ? Ok(status) : BadRequest();
    }

    [HttpPost("{controller}/cancel")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cancel(CancelDebtDto dto)
    {
        var login = GetLogin();
        var status = await _service.CancelAsync(login, dto);

        return status != null ? Ok(status) : BadRequest();
    }

    [HttpPost("{controller}/close")]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(DealStatusType), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Close(CloseDebtDto dto)
    {
        var login = GetLogin();
        var status = await _service.CloseAsync(login, dto);

        return status != null ? Ok(status) : BadRequest();
    }

    private string GetLogin() => User.FindFirstValue("sub");
}