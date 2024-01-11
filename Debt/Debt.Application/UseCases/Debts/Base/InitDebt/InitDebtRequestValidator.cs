using FluentValidation;
using Global;
using Shared.FluentValidation;

namespace DebtManager.Application;

internal class InitDebtRequestValidator<T> : AbstractValidator<T> where T : InitDebtRequest
{
    public InitDebtRequestValidator()
    {
        RuleFor(x => x.Body.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength);

        RuleFor(x => x.Body.Sum)
            .GreaterThanWithMessage(Constraints.Sum.Min - 1);

        RuleFor(x => x.Body.Begin)
            .LessThanOrEqualToWithMessage(DateTime.UtcNow);

        RuleFor(x => x.Body.End)
            .GreaterThanWithMessage(x => x.Body.Begin)
            .When(x => x.Body.End.HasValue);
    }
}