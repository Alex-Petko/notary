using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Application;

public sealed record CreateTokenRequest(
    CreateTokenDto Dto, 
    IResponseCookies Cookies) 
    : IRequest<IActionResult>;
