namespace DealProject.Domain;

public record InitDebtDto(
    DealSourceType Source,
    int GiverId,
    int ReceiverId,
    int Sum,

    DateTime Begin,
    DateTime? End = null
);
