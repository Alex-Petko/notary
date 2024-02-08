using FluentValidation;
using Global;
using Shared.FluentValidation;

namespace AccessControl.Application;

internal sealed class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength);
    }
}
