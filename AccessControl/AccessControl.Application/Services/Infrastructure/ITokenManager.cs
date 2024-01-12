namespace AccessControl.Application;

internal interface ITokenManager
{
    Task UpdateAsync(string login);
}