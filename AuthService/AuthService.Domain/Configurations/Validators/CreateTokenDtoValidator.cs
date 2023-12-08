﻿
using FluentValidation;

namespace AuthService.Domain;

public class CreateTokenDtoValidator : AbstractValidator<CreateTokenDto>
{
    public CreateTokenDtoValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("The login cannot be empty")
            .MaximumLength(64).WithMessage($"The password length must not exceed {64}");

        RuleFor(x => x.PasswordHash);
    }
}