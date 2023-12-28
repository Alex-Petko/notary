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
            .NotEmpty().WithMessage("The login cannot be empty")
            .MaximumLength(128).WithMessage("The password length must not exceed 128");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("The password cannot be empty")
            .MaximumLength(128).WithMessage("The passwordHash length must not exceed 128");
    }
}