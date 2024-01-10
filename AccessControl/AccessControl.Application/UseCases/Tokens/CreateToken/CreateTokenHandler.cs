using AccessControl.Domain;
using AccessControl.Infrastructure;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AccessControl.Application;

internal sealed class CreateTokenHandler : IRequestHandler<CreateTokenRequest, IActionResult>
{
    private readonly ITransactions _transactions;
    private readonly IMapper _mapper;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IOptionsSnapshot<JwtOptions> _jwtOptions;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateTokenHandler(
        ITransactions transactions,
        IDateTimeProvider dateTimeProvider,
        IMapper mapper,
        IOptionsSnapshot<JwtOptions> jwtOptions,
        IHttpContextAccessor httpContextAccessor)
    {
        _transactions = transactions;
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;
        _jwtOptions = jwtOptions;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IActionResult> Handle(CreateTokenRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);

        if (!await _transactions.ContainsAsync(user))
            return new NotFoundResult();

        var key = _jwtOptions.Value.Key;
        var expiresMinutes = _jwtOptions.Value.ExpiresMinutes;
        var expires = _dateTimeProvider.UtcNow.AddMinutes(expiresMinutes);

        var token = new Token(user.Login, expires);
        var jwt = token.CreateJWT(key);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            Expires = expires,
            MaxAge = new TimeSpan(0, expiresMinutes, 0),
        };

        // doto
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("JwtBearer", jwt, cookieOptions);

        return new OkResult();
    }
}