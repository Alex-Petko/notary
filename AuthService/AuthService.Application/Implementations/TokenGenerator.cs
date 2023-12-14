using AuthService.Domain;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Application;

internal class TokenGenerator : ITokenGenerator
{
    private ITransactions _transactions;
    private IOptionsSnapshot<JwtOptions> _jwtOptions;
    private IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenGenerator(
        ITransactions transactions,
        IOptionsSnapshot<JwtOptions> jwtOptions,
        IMapper mapper,
        IDateTimeProvider dateTimeProvider)
    {
        _transactions = transactions;
        _jwtOptions = jwtOptions;
        _mapper = mapper;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<string?> ExecuteAsync(CreateTokenDto dto)
    {
        var user = _mapper.Map<User>(dto);

        if (!await _transactions.ContainsAsync(user))
            return null;

        var claims = new List<Claim>()
        {
            new Claim("sub", dto.Login)
        };

        var bytes = Encoding.UTF8.GetBytes(_jwtOptions.Value.Key);
        var securityKey = new SymmetricSecurityKey(bytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.Value.ExpiresMinutes),
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}
