using AutoFixture.Xunit2;
using DebtManager.Application;
using FluentValidation.TestHelper;
using Shared.FluentValidation;

namespace DebtManager.Tests;

public class DebtStatusRequestValidatorTests
{
    [Fact]
    public async Task DebtId_Null_NotEmptyErrorMessage()
    {
        // Arrange
        var validator = new ChangeDebtStatusCommandValidator<ChangeDebtStatusCommand>();
        var request = new ChangeDebtStatusCommand
        {
            DebtId = default
        };

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.NotEmpty<ChangeDebtStatusCommand, Guid>(x => x.DebtId);
        result.ShouldHaveValidationErrorFor(x => x.DebtId).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Validate_Ok_Ok(ChangeDebtStatusCommand request)
    {
        // Arrange
        var validator = new ChangeDebtStatusCommandValidator<ChangeDebtStatusCommand>();

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}