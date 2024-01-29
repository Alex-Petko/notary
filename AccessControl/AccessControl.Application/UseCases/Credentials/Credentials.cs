using MediatR;

namespace AccessControl.Application;

public record Credentials
{
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
}