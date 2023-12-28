using DealProject.Application;
using FluentValidation;
using Shared.FluentValidation.Extensions;

namespace DealProject;

public class CloseDebtDtoValidator : AbstractValidator<CloseDebtDto>
{
    public CloseDebtDtoValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}