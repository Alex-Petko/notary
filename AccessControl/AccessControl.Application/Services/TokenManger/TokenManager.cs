using AccessControl.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using static AccessControl.Domain.JwtOptions;

namespace AccessControl.Application;

internal sealed class TokenManager : ITokenManager
{
    private readonly ICommandProvider _commandProvider;
    private readonly INowGetService _nowGetService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;

    public TokenManager(
        ICommandProvider commandProvider,
        INowGetService nowGetService,
        IHttpContextAccessor httpContextAccessor,
        IOptionsSnapshot<JwtOptions> jwtOptions)
    {
        _commandProvider = commandProvider;
        _nowGetService = nowGetService;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOptions;
    }

    public async Task UpdateAsync(TokenManagerDto dto, CancellationToken cancellationToken = default)
    {
        var options = _jwtOptions.Value;

        var token = new Token(dto.Login);
        var rt = Sign(token, options.RefreshTokenDetails);

        await _commandProvider.UpdateRefreshToken(dto.Login, rt, _nowGetService.Now, cancellationToken);

        var jwt = Sign(token, options.JSONWebTokenDetails);
        var jwtCookie = CreateCookieOptions(options.JSONWebTokenDetails);
        _httpContextAccessor.HttpContext!.Response.Cookies.Append("JwtBearer", jwt, jwtCookie);

        var rtCookie = CreateCookieOptions(options.RefreshTokenDetails);
        _httpContextAccessor.HttpContext!.Response.Cookies.Append("RT", rt, rtCookie);
    }

    private string Sign(Token token, Details details)
    {
        var key = details.Key;
        var expires = _nowGetService.Now.AddMinutes(details.ExpiresMinutes);

        var tokenSign = token.Sign(key, expires);
        return tokenSign;
    }

    private CookieOptions CreateCookieOptions(Details details)
    {
        var expiresMinutes = details.ExpiresMinutes;
        var expires = _nowGetService.Now.AddMinutes(expiresMinutes);

        return new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = expires,
            MaxAge = new TimeSpan(0, expiresMinutes, 0),
        };
    }
}