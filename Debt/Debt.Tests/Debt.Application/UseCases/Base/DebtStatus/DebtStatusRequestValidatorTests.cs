using AutoFixture.Xunit2;
using DebtManager.Application;
using FluentValidation.TestHelper;
using Shared.FluentValidation.Extensions;

namespace DebtManager.Tests;

public class DebtStatusRequestValidatorTests
{
    [Fact]
    public async Task DebtId_Null_NotEmptyErrorMessage()
    {
        // Arrange
        var validator = new DebtStatusRequestValidator<DebtStatusRequest>();
        var request = new DebtStatusRequest(default);

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        var message = ValidationErrorMessages.NotEmpty<DebtStatusRequest, Guid>(x => x.DebtId);
        result.ShouldHaveValidationErrorFor(x => x.DebtId).WithErrorMessage(message);
    }

    [Theory, AutoData]
    public async Task Validate_Ok_Ok(DebtStatusRequest request)
    {
        // Arrange
        var validator = new DebtStatusRequestValidator<DebtStatusRequest>();

        // Act
        var result = await validator.TestValidateAsync(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}