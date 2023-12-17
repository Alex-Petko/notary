using AuthService.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthService.Controllers;

[Route("[controller]")]
public class TokensController : ControllerBase
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;

    public TokensController(
        ITokenGenerator tokenGenerator, 
        IDateTimeProvider dateTimeProvider,
        IOptionsSnapshot<JwtOptions> jwtOptions)
    {
        _tokenGenerator = tokenGenerator;
        _dateTimeProvider = dateTimeProvider;
        _jwtOptions = jwtOptions;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateTokenDto dto)
    {
        var key = _jwtOptions.Value.Key;
        var expiresMinutes = _jwtOptions.Value.ExpiresMinutes;
        var token = await _tokenGenerator.ExecuteAsync(dto, key, expiresMinutes);

        if (token == null)
            return BadRequest();

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = _dateTimeProvider.UtcNow.AddMinutes(expiresMinutes),
            MaxAge = new TimeSpan(0, expiresMinutes, 0),
        };

        Response.Cookies.Append("JwtBearer", token, cookieOptions);

        return Ok();
    }
}