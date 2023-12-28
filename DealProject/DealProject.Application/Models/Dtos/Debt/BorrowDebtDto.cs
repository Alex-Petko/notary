namespace DealProject.Application;

public record BorrowDebtDto(
    string Login,
    int Sum,

    DateTime Begin,
    DateTime? End = null
);