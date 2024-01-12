using AccessControl.Domain;
using AccessControl.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using static AccessControl.Application.JwtOptions;

namespace AccessControl.Application;

internal class TokenManager : ITokenManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;

    public TokenManager(
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider,
        IHttpContextAccessor httpContextAccessor,
        IOptionsSnapshot<JwtOptions> jwtOptions)
    {
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
        _httpContextAccessor = httpContextAccessor;
        _jwtOptions = jwtOptions;
    }

    public async Task UpdateAsync(string login)
    {
        var now = _dateTimeProvider.UtcNow;
        var options = _jwtOptions.Value;

        var token = new Token(login);
        var rt = Sign(token, options.Rt, now);

        var oldRtEntity = await _unitOfWork.RefreshTokens.FindAsync(login);

        if (oldRtEntity is not null)
        {
            oldRtEntity.Data = rt;
            oldRtEntity.Timestamp = now;
        }
        else
        {
            var newRtEntity = new RefreshToken
            {
                Login = login,
                Data = rt,
                Timestamp = now
            };
            _unitOfWork.RefreshTokens.Add(newRtEntity);
        }

        await _unitOfWork.SaveChangesAsync();

        var jwt = Sign(token, options.Jwt, now);
        var jwtCookie = CreateCookieOptions(options.Jwt);
        _httpContextAccessor.HttpContext!.Response.Cookies.Append("JwtBearer", jwt, jwtCookie);

        var rtCookie = CreateCookieOptions(options.Rt);
        _httpContextAccessor.HttpContext!.Response.Cookies.Append("RT", rt, rtCookie);   
    }

    private string Sign(Token token, Details details, in DateTime now)
    {
        var key = details.Key;
        var expires = now.AddMinutes(details.ExpiresMinutes);

        var tokenSign = token.Sign(key, expires);
        return tokenSign;
    }

    private CookieOptions CreateCookieOptions(JwtOptions.Details details)
    {
        var expiresMinutes = details.ExpiresMinutes;
        var expires = _dateTimeProvider.UtcNow.AddMinutes(expiresMinutes);

        return new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = expires,
            MaxAge = new TimeSpan(0, expiresMinutes, 0),
        };
    }
}
