using FluentValidation;
using Shared.FluentValidation.Extensions;

namespace DebtManager.Application;

internal class DebtStatusRequestValidator<T> : AbstractValidator<T> where T : DebtStatusRequest
{
    public DebtStatusRequestValidator()
    {
        RuleFor(x => x.DebtId)
            .NotEmptyWithMessage();
    }
}