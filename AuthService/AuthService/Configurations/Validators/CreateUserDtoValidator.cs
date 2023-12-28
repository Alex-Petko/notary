using AuthService.Application;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace AuthService;

[ExcludeFromCodeCoverage]
public sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(128);

        RuleFor(x => x.PasswordHash)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(128);
    }
}