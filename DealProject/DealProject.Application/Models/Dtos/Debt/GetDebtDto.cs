using DealProject.Domain;

namespace DealProject.Application;

public class GetDebtDto
{
    public Guid Id { get; set; }

    public string LenderLogin { get; set; } = null!;
    public string BorrowerLogin { get; set; } = null!;
    public int Sum { get; set; }
    public DealStatusType Status { get; set; }
    public DateTime Begin { get; set; }
    public DateTime? End { get; set; } = null;
}