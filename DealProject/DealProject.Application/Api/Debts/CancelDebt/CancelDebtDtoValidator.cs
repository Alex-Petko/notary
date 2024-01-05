using FluentValidation;
using Shared.FluentValidation.Extensions;

namespace DealProject.Application;

internal sealed class CancelDebtDtoValidator : AbstractValidator<CancelDebtDto>
{
    public CancelDebtDtoValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}