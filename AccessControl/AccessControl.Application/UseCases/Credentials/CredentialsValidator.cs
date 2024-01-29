using FluentValidation;
using Global;
using Shared.FluentValidation;

namespace AccessControl.Application;

internal class CredentialsValidator<T> : AbstractValidator<T> where T : Credentials
{
    public CredentialsValidator()
    {
        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Login.MaxLength);

        RuleFor(x => x.Password)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(Constraints.Password.MaxLength);
    }
}