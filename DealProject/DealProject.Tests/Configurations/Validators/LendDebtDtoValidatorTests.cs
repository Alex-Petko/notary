using DealProject.Application;
using FluentValidation.TestHelper;
using Shared.FluentValidation.Extensions;

namespace DealProject.Tests.Configurations.Validators
{
    public class LendDebtDtoValidatorTests
    {
        private DateTime Begin => DateTime.UtcNow;
        private DateTime End => Begin + new TimeSpan(1, 0, 0);

        [Fact]
        public async Task Validate_Ok_Ok()
        {
            // Arrange
            var dto = new LendDebtDto("1", 1, Begin, End);
            var lendDebtDtoValidator = new LendDebtDtoValidator();

            // Act
            var result = await lendDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_EndNull_Ok()
        {
            // Arrange
            var dto = new LendDebtDto("1", 1, Begin, null);
            var lendDebtDtoValidator = new LendDebtDtoValidator();

            // Act
            var result = await lendDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_LoginEmpty_EmptyErrorMessage()
        {
            // Arrange
            var lendDebtDtoValidator = new LendDebtDtoValidator();
            var dto = new LendDebtDto(null!, 1, Begin);

            // Act
            var result = await lendDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.NotEmpty<LendDebtDto, string>(x => x.Login);
            result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_LoginMoreThan128_MaximumLengthErrorMessage()
        {
            // Arrange
            var lendDebtDtoValidator = new LendDebtDtoValidator();
            var dto = new LendDebtDto(new string('x', 129), 1, Begin);

            // Act
            var result = await lendDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.MaximumLength<LendDebtDto>(x => x.Login, 128);
            result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_Sum0_GreaterThanErrorMessage()
        {
            // Arrange
            var lendDebtDtoValidator = new LendDebtDtoValidator();
            var dto = new LendDebtDto("1", 0, Begin);

            // Act
            var result = await lendDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.GreaterThan<LendDebtDto, int>(x => x.Sum, 0);
            result.ShouldHaveValidationErrorFor(x => x.Sum).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_BeginMoreThanNow_LessThanOrEqualToErrorMessage()
        {
            // Arrange
            var lendDebtDtoValidator = new LendDebtDtoValidator();
            var dto = new LendDebtDto("1", 1, End);

            // Act
            var result = await lendDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.LessThanOrEqualTo<LendDebtDto, DateTime>(x => x.Begin, Begin);
            result.ShouldHaveValidationErrorFor(x => x.Begin).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_BeginMoreThanEnd_GreaterThanErrorMessage()
        {
            // Arrange
            var lendDebtDtoValidator = new LendDebtDtoValidator();
            var utc = Begin;
            var dto = new LendDebtDto("1", 0, utc, utc);

            // Act
            var result = await lendDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.GreaterThan<LendDebtDto, DateTime>(x => x.End, x => x.Begin);
            result.ShouldHaveValidationErrorFor(x => x.End).WithErrorMessage(message);
        }
    }
}