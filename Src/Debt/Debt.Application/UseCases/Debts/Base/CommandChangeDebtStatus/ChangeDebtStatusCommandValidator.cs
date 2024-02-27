using FluentValidation;
using Global;
using Shared.FluentValidation;

namespace DebtManager.Application;

internal class ChangeDebtStatusCommandValidator<T> : AbstractValidator<T> where T : ChangeDebtStatusCommand
{
    public ChangeDebtStatusCommandValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();

        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength);
    }
}