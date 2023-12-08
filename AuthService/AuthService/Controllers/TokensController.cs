using AuthService.Domain;
using AuthService.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Controllers;

[ApiController]
public class TokensController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;
    private readonly IMapper _mapper;

    public TokensController(IRepository repository, IOptionsSnapshot<JwtOptions> jwtOptions, IMapper mapper)
    {
        _repository = repository;
        _jwtOptions = jwtOptions;
        _mapper = mapper;
    }

    [HttpPost("[controller]")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateTokenDto dto)
    {
        var user = _mapper.Map<User>(dto);
        if (! await _repository.Users.Contains(user))
            return BadRequest();

        var claims = new List<Claim>()
        {
            new Claim("sub", dto.Login)
        };
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.Value.ExpiresMinutes),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return Ok(token);
    }
}