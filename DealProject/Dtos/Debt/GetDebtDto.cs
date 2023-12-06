namespace DealProject.Entities;

public record GetDebtDto(
    int GiverId,
    int ReceiverId,
    int Sum,

    DateTime Begin,
    DateTime? End = null
);