using System.Diagnostics.CodeAnalysis;

namespace DebtManager.Domain;

[ExcludeFromCodeCoverage]
public sealed class Debt
{
    private DateTime? _begin;
    private DateTime? _end = null;

    public Guid Id { get; set; }

    public string LenderLogin { get; set; } = null!;

    public string BorrowerLogin { get; set; } = null!;

    public int Sum { get; set; }

    public DealStatusType Status { get; set; }

    public DateTime Begin 
    { 
        get => _begin ??= DateTime.UtcNow; 
        set => _begin = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
    }

    public DateTime? End 
    { 
        get => _end; 
        set => _end = value is not null 
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) 
            : null; 
    }
}