namespace AccessControl.Application;

internal interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}