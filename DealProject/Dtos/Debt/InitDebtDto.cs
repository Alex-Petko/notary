using System.ComponentModel.DataAnnotations;

namespace DealProject.Entities;

public record InitDebtDto(
    [Required]
    DealSourceType Source,
    int GiverId,
    int ReceiverId,
    int Sum,

    DateTime Begin,
    DateTime? End = null
);
