namespace DealProject.Application;

public sealed class LendDebtDto
{
    private string _login = null!;
    private int _sum;
    private DateTime _begin;
    private DateTime? _end;
   
    public string Login
    {
        get => _login;
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(Login));

            if (_login.Length > 128)
                throw new ArgumentOutOfRangeException(nameof(Login));
            
            _login = value;
        }
    }

    public int Sum 
    { 
        get => _sum; 
        set => _sum = value > 0 
            ? value 
            : throw new ArgumentOutOfRangeException(nameof(Sum)); 
    }

    public DateTime Begin
    {
        get => _begin;
        set => _begin = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }

    public DateTime? End
    {
        get => _end;
        set => _end = value is not null
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
            : null;
    }

    public LendDebtDto(
        string login,
        int sum,
        DateTime? begin = null,
        DateTime? end = null)
    {
        Login = login;
        Sum = sum;
        Begin = begin ?? DateTime.UtcNow;
        End = end;
    }
}