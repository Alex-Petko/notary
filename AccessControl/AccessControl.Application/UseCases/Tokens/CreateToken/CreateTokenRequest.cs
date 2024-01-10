namespace AccessControl.Application;

public sealed record CreateTokenRequest(
    string Login,
    string Password)
    : RequestBase(Login, Password);
