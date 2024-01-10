using FluentValidation;
using Global;
using Shared.FluentValidation.Extensions;

namespace AccessControl.Application;

internal class RequestBaseValidator<T> : AbstractValidator<T> where T : RequestBase
{
    public RequestBaseValidator()
    {
        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength);

        RuleFor(x => x.Password)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Password.MaxLength);
    }
}