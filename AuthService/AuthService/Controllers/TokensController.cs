using AuthService.Application;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[Route("[controller]")]
public class TokensController : ControllerBase
{
    private readonly ITokenGenerator _tokenGenerator;

    public TokensController(ITokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateTokenDto dto)
    {
        var token = await _tokenGenerator.ExecuteAsync(dto);

        if (token == null)
            return BadRequest();

        Response.Cookies.Append("JwtBearer", token);
        return Ok();
    }
}