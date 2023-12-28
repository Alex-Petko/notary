using AuthService.Application;
using FluentValidation.TestHelper;
using Shared.FluentValidation.Extensions;

namespace AuthService.Tests.Configurations.Validators
{
    public class CreateUserDtoValidatorTests
    {
        [Fact]
        public async Task Validate_Ok_Ok()
        {
            // Arrange
            var createUserDtoValidator = new CreateUserDtoValidator();

            var userDto = new CreateUserDto("1", "2");
            // Act
            var result = await createUserDtoValidator.TestValidateAsync(userDto);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public async Task Validate_LoginNull_NotEmptyErrorMessage()
        {
            // Arrange
            var createTokenDtoValidator = new CreateUserDtoValidator();
            var dto = new CreateUserDto(null!, "2");

            // Act
            var result = await createTokenDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.NotEmpty<CreateUserDto, string>(x => x.Login);
            result.ShouldHaveValidationErrorFor(x => x.Login)
                .WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_LoginMoreThen128_MaximumLengthErrorMessage()
        {
            // Arrange
            var createTokenDtoValidator = new CreateUserDtoValidator();
            var dto = new CreateUserDto(new string('x', 129), "2");

            // Act
            var result = await createTokenDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.MaximumLength<CreateUserDto, string>(x => x.Login, 128);
            result.ShouldHaveValidationErrorFor(x => x.Login)
                .WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_PasswordHashNull_NotEmptyErrorMessage()
        {
            // Arrange
            var createTokenDtoValidator = new CreateUserDtoValidator();
            var dto = new CreateUserDto("1", null!);

            // Act
            var result = await createTokenDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.NotEmpty<CreateUserDto, string>(x => x.PasswordHash);
            result.ShouldHaveValidationErrorFor(x => x.PasswordHash)
                .WithErrorMessage(message);
        }

        [Fact]
        public async Task Validate_PasswordHashMoreThen128_MaximumLengthErrorMessage()
        {
            // Arrange
            var createTokenDtoValidator = new CreateUserDtoValidator();
            var dto = new CreateUserDto("1", new string('x', 129));

            // Act
            var result = await createTokenDtoValidator.TestValidateAsync(dto);

            // Assert
            var message = ValidationErrorMessages.MaximumLength<CreateUserDto, string>(x => x.PasswordHash, 128);
            result.ShouldHaveValidationErrorFor(x => x.PasswordHash)
                .WithErrorMessage(message);
        }
    }
}