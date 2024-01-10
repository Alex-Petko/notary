using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Application;

public record RequestBase(
    string Login,
    string Password)
    : IRequest<IActionResult>;