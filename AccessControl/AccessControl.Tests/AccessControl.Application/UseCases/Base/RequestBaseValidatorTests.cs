using AccessControl.Application;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Global;
using Shared.FluentValidation.Extensions;
using Shared.Tests;

namespace AccessControl.Tests;

public class RequestBaseValidatorTests
{
    private const int LoginMaxLength = Constraints.Login.MaxLength;
    private const int PasswordMaxLength = Constraints.Password.MaxLength;

    [Theory, AutoData]
    public async Task Login_GreaterThanMax_MaximumLengthErrorMessage(string password)
    {
        // Arrange
        var validator = new RequestBaseValidator<RequestBase>();
        var request = new RequestBase(TestHelper.String(LoginMaxLength + 1), password);

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.MaximumLength<RequestBase>(x => x.Login, LoginMaxLength);
        result.ShouldHaveValidationErrorFor(x => x.Login)
            .WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Login_Null_NotEmptyErrorMessage(string password)
    {
        // Arrange
        var validator = new RequestBaseValidator<RequestBase>();
        var request = new RequestBase(null!, password);

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.NotEmpty<RequestBase, string>(x => x.Login);
        result.ShouldHaveValidationErrorFor(x => x.Login)
            .WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Password_GreaterThanMax_MaximumLengthErrorMessage(string login)
    {
        // Arrange
        var validator = new RequestBaseValidator<RequestBase>();
        var request = new RequestBase(login, TestHelper.String(PasswordMaxLength + 1));

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.MaximumLength<RequestBase>(x => x.Password, PasswordMaxLength);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Password_Null_NotEmptyErrorMessage(string login)
    {
        // Arrange
        var validator = new RequestBaseValidator<RequestBase>();
        var request = new RequestBase(login, null!);

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.NotEmpty<RequestBase, string>(x => x.Password);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Validate_Ok_Ok(string login, string password)
    {
        // Arrange
        var validator = new RequestBaseValidator<RequestBase>();
        var request = new RequestBase(login, password);

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}