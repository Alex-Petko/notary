namespace AuthService.Application;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}