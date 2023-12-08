using FluentValidation;

namespace DealProject.Domain;

public class InitDebtValidator : AbstractValidator<InitDebtDto>
{
    public InitDebtValidator()
    {
        RuleFor(x => x.Source)
            .IsInEnum();

        RuleFor(x => x.GiverId)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.ReceiverId)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.ReceiverId)
            .NotEqual(x => x.GiverId);

        RuleFor(x => x.Sum)
            .GreaterThan(0);

        RuleFor(x => x.Begin)
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.End)
            .GreaterThan(x => x.Begin)
            .When(x => x.End.HasValue);
    }
}
