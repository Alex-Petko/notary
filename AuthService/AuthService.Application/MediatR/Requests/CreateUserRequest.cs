using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Application;

public sealed record CreateUserRequest(
    CreateUserDto Dto)
    : IRequest<IActionResult>;