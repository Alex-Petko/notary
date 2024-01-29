using FluentValidation;
using Shared.FluentValidation;

namespace DebtManager.Application;

internal sealed class GetDebtQueryValidator : AbstractValidator<GetDebtQuery>
{
    public GetDebtQueryValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}