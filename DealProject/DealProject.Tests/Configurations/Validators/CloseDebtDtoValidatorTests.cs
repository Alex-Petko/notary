using DealProject.Application;
using FluentValidation.TestHelper;
using Shared.FluentValidation.Extensions;

namespace DealProject.Tests.Configurations.Validators
{
    public class CloseDebtDtoValidatorTests
    {
        [Fact]
        public async Task Validate_Ok_Ok()
        {
            // Arrange
            var closeDebtDtoValidator = new CloseDebtDtoValidator();
            var dto = new CloseDebtDto(Guid.NewGuid());

            // Act
            var result = await closeDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_DebtIdNull_NotEmptyErrorMessage()
        {
            // Arrange
            var closeDebtDtoValidator = new CloseDebtDtoValidator();
            var dto = new CloseDebtDto(default);

            // Act
            var result = await closeDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.NotEmpty<CloseDebtDto, Guid>(x => x.DebtId);
            result.ShouldHaveValidationErrorFor(x => x.DebtId).WithErrorMessage(message);
        }
    }
}