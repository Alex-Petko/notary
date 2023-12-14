using DealProject.Application;
using FluentValidation;

namespace DealProject;

public class CloseDebtDtoValidator : AbstractValidator<CloseDebtDto>
{
    public CloseDebtDtoValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmpty();
    }
}
