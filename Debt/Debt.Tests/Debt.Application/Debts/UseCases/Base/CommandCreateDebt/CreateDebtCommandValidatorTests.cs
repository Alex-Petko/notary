using AutoFixture.Xunit2;
using DebtManager.Application;
using FluentValidation.TestHelper;
using Global;
using Shared.FluentValidation;
using Shared.Tests;

namespace DebtManager.Tests;

public class CreateDebtCommandValidatorTests
{
    private const int LoginMaxLength = Constraints.Login.MaxLength;
    private const int SumMin = Constraints.Sum.Min;

    private DateTime Begin => DateTime.UtcNow;
    private DateTime End => Begin + new TimeSpan(1, 0, 0);

    [Theory, AutoData]
    public async Task Begin_EqualToEnd_GreaterThanErrorMessage(string login, int sum)
    {
        // Arrange
        var validator = new CreateDebtCommandValidator<CreateDebtCommand>();
        var now = Begin;

        var request = new CreateDebtCommand
        {
            Body = new(login, sum, now, now)
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.GreaterThan<CreateDebtCommand, DateTime>(x => x.Body.End, x => x.Body.Begin);
        result.ShouldHaveValidationErrorFor(x => x.Body.End).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Begin_GreaterThanNow_LessThanOrEqualToErrorMessage(string login, int sum)
    {
        // Arrange
        var validator = new CreateDebtCommandValidator<CreateDebtCommand>();

        var request = new CreateDebtCommand
        {
            Body = new(login, sum, End)
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.LessThanOrEqualTo<CreateDebtCommand, DateTime>(x => x.Body.Begin, Begin);
        result.ShouldHaveValidationErrorFor(x => x.Body.Begin).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task End_Null_Ok(string login, int sum)
    {
        // Arrange
        var request = new CreateDebtCommand
        {
            Body = new(login, sum, Begin, null)
        };

        var validator = new CreateDebtCommandValidator<CreateDebtCommand>();

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory, AutoData]
    public async Task Login_Empty_EmptyErrorMessage(int sum)
    {
        // Arrange
        var validator = new CreateDebtCommandValidator<CreateDebtCommand>();

        var request = new CreateDebtCommand
        {
            Body = new(null!, sum, Begin)
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.NotEmpty<CreateDebtCommand, string>(x => x.Body.Login);
        result.ShouldHaveValidationErrorFor(x => x.Body.Login).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Login_GreaterThanMax_MaximumLengthErrorMessage(int sum)
    {
        // Arrange
        var validator = new CreateDebtCommandValidator<CreateDebtCommand>();

        var request = new CreateDebtCommand
        {
            Body = new(TestHelper.String(LoginMaxLength + 1), sum, Begin)
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.MaximumLength<CreateDebtCommand>(x => x.Body.Login, LoginMaxLength);
        result.ShouldHaveValidationErrorFor(x => x.Body.Login).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Sum_LessThanMin_GreaterThanErrorMessage(string login)
    {
        // Arrange
        var validator = new CreateDebtCommandValidator<CreateDebtCommand>();

        var request = new CreateDebtCommand
        {
            Body = new(login, SumMin - 1, Begin, End)
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.GreaterThan<CreateDebtCommand, int>(x => x.Body.Sum, SumMin - 1);
        result.ShouldHaveValidationErrorFor(x => x.Body.Sum).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Validate_Ok_Ok(string login, int sum)
    {
        // Arrange
        var request = new CreateDebtCommand
        {
            Body = new(login, sum, Begin, End)
        };

        var validator = new CreateDebtCommandValidator<CreateDebtCommand>();

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}