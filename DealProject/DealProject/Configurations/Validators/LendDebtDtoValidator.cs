using DealProject.Application;
using FluentValidation;

namespace DealProject;

public class LendDebtDtoValidator : AbstractValidator<LendDebtDto>
{
    public LendDebtDtoValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("The login cannot be empty")
            .MaximumLength(128).WithMessage("The password length must not exceed 128");

        RuleFor(x => x.Sum)
            .GreaterThan(0).WithMessage("The sum must be greater than 0");

        RuleFor(x => x.Begin)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("The begin must be less than or equal to 0");

        RuleFor(x => x.End)
            .GreaterThan(x => x.Begin)
            .When(x => x.End.HasValue).WithMessage("The end must be greater than the begin");
    }
}
