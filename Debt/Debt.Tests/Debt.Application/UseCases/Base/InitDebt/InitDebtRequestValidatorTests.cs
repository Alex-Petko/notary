using AutoFixture.Xunit2;
using DebtManager.Application;
using FluentValidation.TestHelper;
using Global;
using Shared.FluentValidation.Extensions;
using Shared.Tests;

namespace DebtManager.Tests;

public class InitDebtRequestValidatorTests
{
    private const int LoginMaxLength = Constraints.Login.MaxLength;
    private const int SumMin = Constraints.Sum.Min;

    private DateTime Begin => DateTime.UtcNow;
    private DateTime End => Begin + new TimeSpan(1, 0, 0);

    [Theory, AutoData]
    public async Task Begin_EqualToEnd_GreaterThanErrorMessage(string login, int sum)
    {
        // Arrange
        var validator = new InitDebtRequestValidator<InitDebtRequest>();
        var now = Begin;

        var request = new InitDebtRequest
        {
            Login = login,
            Sum = sum,
            Begin = now,
            End = now,
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.GreaterThan<InitDebtRequest, DateTime>(x => x.End, x => x.Begin);
        result.ShouldHaveValidationErrorFor(x => x.End).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Begin_GreaterThanNow_LessThanOrEqualToErrorMessage(string login, int sum)
    {
        // Arrange
        var validator = new InitDebtRequestValidator<InitDebtRequest>();

        var request = new InitDebtRequest
        {
            Login = login,
            Sum = sum,
            Begin = End,
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.LessThanOrEqualTo<InitDebtRequest, DateTime>(x => x.Begin, Begin);
        result.ShouldHaveValidationErrorFor(x => x.Begin).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task End_Null_Ok(string login, int sum)
    {
        // Arrange
        var request = new InitDebtRequest
        {
            Login = login,
            Sum = sum,
            Begin = Begin,
            End = null,
        };

        var validator = new InitDebtRequestValidator<InitDebtRequest>();

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory, AutoData]
    public async Task Login_Empty_EmptyErrorMessage(int sum)
    {
        // Arrange
        var validator = new InitDebtRequestValidator<InitDebtRequest>();

        var request = new InitDebtRequest
        {
            Login = null!,
            Sum = sum,
            Begin = Begin,
            End = End,
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.NotEmpty<InitDebtRequest, string>(x => x.Login);
        result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Login_GreaterThanMax_MaximumLengthErrorMessage(int sum)
    {
        // Arrange
        var validator = new InitDebtRequestValidator<InitDebtRequest>();

        var request = new InitDebtRequest
        {
            Login = TestHelper.String(LoginMaxLength + 1),
            Sum = sum,
            Begin = Begin,
            End = End,
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.MaximumLength<InitDebtRequest>(x => x.Login, LoginMaxLength);
        result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Sum_LessThanMin_GreaterThanErrorMessage(string login)
    {
        // Arrange
        var validator = new InitDebtRequestValidator<InitDebtRequest>();

        var request = new InitDebtRequest
        {
            Login = login,
            Sum = SumMin - 1,
            Begin = Begin,
            End = End,
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.GreaterThan<InitDebtRequest, int>(x => x.Sum, SumMin - 1);
        result.ShouldHaveValidationErrorFor(x => x.Sum).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Validate_Ok_Ok(string login, int sum)
    {
        // Arrange
        var request = new InitDebtRequest
        {
            Login = login,
            Sum = sum,
            Begin = Begin,
            End = End,
        };

        var validator = new InitDebtRequestValidator<InitDebtRequest>();

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}