namespace DealProject.Application;

public record LendDebtDto(
    string Login,
    int Sum,

    DateTime Begin,
    DateTime? End = null
);