using AccessControl.Application;
using AutoFixture.Xunit2;
using FluentValidation.TestHelper;
using Shared.FluentValidation;
using Shared.Tests;
using static Global.Constraints;

namespace Application.UseCases;

public class RequestBaseValidatorTests
{
    private const int LoginMaxLength = Login.MaxLength;
    private const int PasswordMaxLength = Password.MaxLength;

    [Theory, AutoData]
    public async Task Login_GreaterThanMax_MaximumLengthErrorMessage(string password)
    {
        // Arrange
        var validator = new RequestBaseValidator<RequestBase>();
        var request = new RequestBase
        {
            Login = TestHelper.String(LoginMaxLength + 1),
            Password = password
        };

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
        var request = new RequestBase
        {
            Login = null!,
            Password = password
        };

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
        var request = new RequestBase
        {
            Login = login,
            Password = TestHelper.String(PasswordMaxLength + 1)
        };

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
        var request = new RequestBase
        {
            Login = login,
            Password = null!
        };

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
        var request = new RequestBase
        {
            Login = login,
            Password = password
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}