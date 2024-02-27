using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccessControl.Application;

internal sealed class Token
{
    private readonly IEnumerable<Claim> _claims;

    public Token(string login)
    {
        _claims = new Claim[]
        {
            new Claim("sub", login)
        };
    }

    public string Sign(string key, in DateTime expires)
    {
        var bytes = Encoding.UTF8.GetBytes(key);
        var securityKey = new SymmetricSecurityKey(bytes);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            claims: _claims,
            expires: expires,
            signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}