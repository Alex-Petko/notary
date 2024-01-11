using FluentValidation;
using Shared.FluentValidation;

namespace DebtManager.Application;

internal sealed class GetDebtRequestValidator : AbstractValidator<GetDebtRequest>
{
    public GetDebtRequestValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}