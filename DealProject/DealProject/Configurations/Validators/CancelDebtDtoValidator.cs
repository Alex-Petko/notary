using DealProject.Application;
using FluentValidation;

namespace DealProject;

public class CancelDebtDtoValidator : AbstractValidator<CancelDebtDto>
{
    public CancelDebtDtoValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmpty();
    }
}