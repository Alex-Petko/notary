using System.Diagnostics.CodeAnalysis;

namespace DealProject.Domain;

[ExcludeFromCodeCoverage]
public sealed class Debt
{
    private DateTime begin;
    private DateTime? end = null;

    public Guid Id { get; set; }

    public string LenderLogin { get; set; } = null!;

    public string BorrowerLogin { get; set; } = null!;

    public int Sum { get; set; }

    public DealStatusType Status { get; set; }

    public DateTime Begin 
    { 
        get => begin; 
        set => begin = DateTime.SpecifyKind(value, DateTimeKind.Utc); 
    }

    public DateTime? End 
    { 
        get => end; 
        set => end = value is not null 
            ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) 
            : null; 
    }
}