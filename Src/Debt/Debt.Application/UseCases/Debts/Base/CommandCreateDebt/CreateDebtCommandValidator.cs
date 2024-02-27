using FluentValidation;
using Global;
using Shared.FluentValidation;

namespace DebtManager.Application;

internal class CreateDebtCommandValidator<T> : AbstractValidator<T> where T : CreateDebtCommand
{
    public CreateDebtCommandValidator()
    {
        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength);

        RuleFor(x => x.Body.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength)
            .When(x => x.Body is not null);

        RuleFor(x => x.Body.Sum)
            .GreaterThanWithMessage(Constraints.Sum.Min - 1)
            .When(x => x.Body is not null);

        RuleFor(x => x.Body.Begin)
            .LessThanOrEqualToWithMessage(DateTimeOffset.UtcNow.AddMinutes(5))
            .When(x => x.Body is not null);

        RuleFor(x => x.Body.End)
            .GreaterThanWithMessage(x => x.Body.Begin)
            .When(x => x.Body is not null && x.Body.End.HasValue);
    }
}