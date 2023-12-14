using System.Diagnostics.CodeAnalysis;

namespace DealProject.Domain;

[ExcludeFromCodeCoverage]
public class Debt
{
    public Guid Id { get; set; }
    public string LenderLogin { get; set; } = null!;
    public string BorrowerLogin { get; set; } = null!;
    public int Sum { get; set; }
    public DealStatusType Status { get; set; }
    public DateTime Begin { get; set; }
    public DateTime? End { get; set; } = null;
}
