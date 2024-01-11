using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccessControl.Application;

public record RequestBase : IRequest<IActionResult>
{
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
}