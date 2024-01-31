using AutoFixture;
using DebtManager.Application;
using FluentValidation.TestHelper;
using Shared.Tests;
using static Global.Constraints;

namespace DebtManager.Tests;

public class ChangeDebtStatusCommandValidatorTests
{
    private const int LoginMaxLength = Login.MaxLength;

    [Fact]
    public async Task DebtId_Null_NotEmptyErrorMessage()
    {
        // Arrange
        var validator = new ChangeDebtStatusCommandValidator<ChangeDebtStatusCommand>();
        var command = GetCorrectObjectToTest() with
        {
            DebtId = default
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveNotEmptyError(x => x.DebtId);
    }

    [Fact]
    public async Task Login_Null_NotEmptyErrorMessage()
    {
        // Arrange
        var validator = new ChangeDebtStatusCommandValidator<ChangeDebtStatusCommand>();
        var command = GetCorrectObjectToTest() with
        {
            Login = null!
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert

        result.ShouldHaveNotEmptyError(x => x.Login);
    }

    [Fact]
    public async Task Login_GreaterThanMax_MaximumLengthErrorMessage()
    {
        // Arrange
        var validator = new ChangeDebtStatusCommandValidator<ChangeDebtStatusCommand>();
        var command = GetCorrectObjectToTest() with
        {
            Login = TestHelper.String(LoginMaxLength + 1)
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveMaximumLengthError(x => x.Login, LoginMaxLength);
    }

    [Fact]
    public async Task Validate_Ok_Ok()
    {
        // Arrange
        var validator = new ChangeDebtStatusCommandValidator<ChangeDebtStatusCommand>();
        var command = GetCorrectObjectToTest();

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    private ChangeDebtStatusCommand GetCorrectObjectToTest()
    {
        var fixture = new Fixture();
        var randomLoginString = new RandomString(0, LoginMaxLength);
        fixture.Customizations.Add(new StringGenerator(() => randomLoginString));

        var login = fixture.Create<string>();
        return new ChangeDebtStatusCommand()
        {
            DebtId = Guid.NewGuid(),
            Login = login
        };
    }
}
