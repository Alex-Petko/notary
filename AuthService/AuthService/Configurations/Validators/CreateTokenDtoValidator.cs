using AuthService.Application;
using FluentValidation;
using Shared.FluentValidation.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace AuthService;

[ExcludeFromCodeCoverage]
public sealed class CreateTokenDtoValidator : AbstractValidator<CreateTokenDto>
{
    public CreateTokenDtoValidator()
    {
        RuleFor(x => x.Login)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(128);

        RuleFor(x => x.PasswordHash)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(128); ;
    }
}