using DealProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DealProject.Controllers;

[ApiController]
public class DebtsController : ControllerBase
{
    [HttpGet("{controller}")]
    [ProducesResponseType(typeof(IEnumerable<GetDebtDto>), StatusCodes.Status200OK)]
    public IActionResult Get()
    {
        var answer = new[]
        {
            new GetDebtDto(1, 2, 100, new DateTime(2023, 12, 01)),
            new GetDebtDto(2, 1, 200, new DateTime(2023, 12, 01))
        };

        return Ok(answer);
    }

    [HttpGet("{controller}/{id}")]
    [ProducesResponseType(typeof(GetDebtDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(int id)
    {
        if (id != 1 && id != 2)
            return NotFound();

        var answer = new GetDebtDto(1, 2, 100, new DateTime(2023, 12, 01));

        return Ok(answer);
    }

    [HttpPost("{controller}")]
    [ProducesResponseType(typeof(GetCreatedDebtDto), StatusCodes.Status201Created)]
    public IActionResult Init(InitDebtDto dto)
    {
         return Created("", new GetCreatedDebtDto(1));
    }

    //[HttpPost("{controller}")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public IActionResult Close(CloseDebtDto dto)
    //{
    //    return Ok();
    //}
}
