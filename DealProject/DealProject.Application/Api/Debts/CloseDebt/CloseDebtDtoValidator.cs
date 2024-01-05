using FluentValidation;
using Shared.FluentValidation.Extensions;

namespace DealProject.Application;

internal sealed class CloseDebtDtoValidator : AbstractValidator<CloseDebtDto>
{
    public CloseDebtDtoValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}