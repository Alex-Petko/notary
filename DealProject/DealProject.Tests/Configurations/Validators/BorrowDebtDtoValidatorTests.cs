using DealProject.Application;
using FluentValidation.TestHelper;
using Shared.FluentValidation.Extensions;

namespace DealProject.Tests.Configurations.Validators
{
    public class BorrowDebtDtoValidatorTests
    {
        private DateTime Begin => DateTime.UtcNow;
        private DateTime End => Begin + new TimeSpan(1, 0, 0);

        [Fact]
        public async Task Validate_Ok_Ok()
        {
            // Arrange
            var dto = new BorrowDebtDto("1", 1, Begin, End);
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_EndNull_Ok()
        {
            // Arrange
            var dto = new BorrowDebtDto("1", 1, Begin, null);
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_BeginNull_UtcNow()
        {
            // Arrange
            var dto = new BorrowDebtDto("1", 1, null);
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_LoginEmpty_EmptyErrorMessage()
        {
            // Arrange
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();
            var dto = new BorrowDebtDto(null!, 1, Begin);

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.NotEmpty<BorrowDebtDto, string>(x => x.Login);
            result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_LoginMoreThan128_MaximumLengthErrorMessage()
        {
            // Arrange
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();
            var dto = new BorrowDebtDto(new string('x', 129), 1, Begin);

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.MaximumLength<BorrowDebtDto>(x => x.Login, 128);
            result.ShouldHaveValidationErrorFor(x => x.Login).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_Sum0_GreaterThanErrorMessage()
        {
            // Arrange
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();
            var dto = new BorrowDebtDto("1", 0, Begin);

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.GreaterThan<BorrowDebtDto, int>(x => x.Sum, 0);
            result.ShouldHaveValidationErrorFor(x => x.Sum).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_BeginMoreThanNow_LessThanOrEqualToErrorMessage()
        {
            // Arrange
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();
            var dto = new BorrowDebtDto("1", 1, End);

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.LessThanOrEqualTo<BorrowDebtDto, DateTime>(x => x.Begin, Begin);
            result.ShouldHaveValidationErrorFor(x => x.Begin).WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_BeginMoreThanEnd_GreaterThanErrorMessage()
        {
            // Arrange
            var borrowDebtDtoValidator = new BorrowDebtDtoValidator();
            var utc = Begin;
            var dto = new BorrowDebtDto("1", 0, utc, utc);

            // Act
            var result = await borrowDebtDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.GreaterThan<BorrowDebtDto, DateTime>(x => x.End, x => x.Begin);
            result.ShouldHaveValidationErrorFor(x => x.End).WithErrorMessage(message);
        }
    }
}