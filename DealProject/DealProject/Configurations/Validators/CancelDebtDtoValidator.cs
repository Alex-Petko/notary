using DealProject.Application;
using FluentValidation;
using Shared.FluentValidation.Extensions;

namespace DealProject;

public class CancelDebtDtoValidator : AbstractValidator<CancelDebtDto>
{
    public CancelDebtDtoValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}