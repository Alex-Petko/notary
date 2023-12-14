namespace AuthService.Application;

internal interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}