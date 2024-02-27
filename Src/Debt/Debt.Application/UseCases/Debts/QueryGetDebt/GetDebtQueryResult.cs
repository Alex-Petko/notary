using DebtManager.Domain;

namespace DebtManager.Application;

public sealed class GetDebtQueryResult
{
    public Guid Id { get; set; }

    public string LenderLogin { get; set; } = null!;

    public string BorrowerLogin { get; set; } = null!;

    public int Sum { get; set; }

    public DealStatusType Status { get; set; }

    public DateTimeOffset Begin { get; set; }

    public DateTimeOffset? End { get; set; } = null;
}