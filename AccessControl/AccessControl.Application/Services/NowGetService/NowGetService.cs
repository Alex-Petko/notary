namespace AccessControl.Application;

internal sealed class NowGetService : INowGetService
{
    private DateTime? _now;

    public DateTime Now =>  _now ??= DateTime.UtcNow;
}