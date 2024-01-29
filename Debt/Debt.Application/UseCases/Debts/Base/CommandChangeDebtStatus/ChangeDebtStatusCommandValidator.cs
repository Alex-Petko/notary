using FluentValidation;
using Shared.FluentValidation;

namespace DebtManager.Application;

internal class ChangeDebtStatusCommandValidator<T> : AbstractValidator<T> where T : ChangeDebtStatusCommand
{
    public ChangeDebtStatusCommandValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}