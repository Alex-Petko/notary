using FluentValidation;
using Global;
using Shared.FluentValidation.Extensions;

namespace DebtManager.Application;

internal class InitDebtRequestValidator<T> : AbstractValidator<T> where T : InitDebtRequest
{
    public InitDebtRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength);

        RuleFor(x => x.Sum)
            .GreaterThanWithMessage(Constraints.Sum.Min - 1);

        RuleFor(x => x.Begin)
            .LessThanOrEqualToWithMessage(DateTime.UtcNow);

        RuleFor(x => x.End)
            .GreaterThanWithMessage(x => x.Begin)
            .When(x => x.End.HasValue);
    }
}
