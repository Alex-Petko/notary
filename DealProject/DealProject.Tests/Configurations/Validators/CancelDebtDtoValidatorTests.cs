using DealProject.Application;
using FluentValidation.TestHelper;
using Shared.FluentValidation.Extensions;

namespace DealProject.Tests.Configurations.Validators
{
    public class CancelDebtDtoValidatorTests
    {
        [Fact]
        public async Task Validate_Ok_Ok()
        {
            // Arrange
            var cancelDebtDtoValidator = new CancelDebtDtoValidator();
            var dto = new CancelDebtDto(Guid.NewGuid());

            // Act
            var result = await cancelDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_DebtIdNull_NotEmptyErrorMessage()
        {
            // Arrange
            var cancelDebtDtoValidator = new CancelDebtDtoValidator();
            var dto = new CancelDebtDto(default);

            // Act
            var result = await cancelDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.NotEmpty<CancelDebtDto, Guid>(x => x.DebtId);
            result.ShouldHaveValidationErrorFor(x => x.DebtId).WithErrorMessage(message);
        }
    }
}