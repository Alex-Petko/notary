namespace AccessControl.Application;

public sealed record CreateUserRequest(
    string Login,
    string Password)
    : RequestBase(Login, Password);