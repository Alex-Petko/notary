namespace DebtManager.Domain;

public sealed class Debt
{
    public Guid Id { get; set; }

    public string LenderLogin { get; set; } = null!;

    public string BorrowerLogin { get; set; } = null!;

    public int Sum { get; set; }

    public DealStatusType Status { get; set; }

    public DateTimeOffset Begin { get; set; }

    public DateTimeOffset? End { get; set; }
}
