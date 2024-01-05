using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Application;

internal sealed class Token
{
    private readonly IEnumerable<Claim> _claims;
    private readonly DateTime _expires;

    public Token(string login, DateTime expires)
    {
        _claims = new Claim[]
        {
            new Claim("sub", login)
        };

        _expires = expires;
    }

    public string CreateJWT(string key)
    {
        var bytes = Encoding.UTF8.GetBytes(key);
        var securityKey = new SymmetricSecurityKey(bytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            claims: _claims,
            expires: _expires,
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}