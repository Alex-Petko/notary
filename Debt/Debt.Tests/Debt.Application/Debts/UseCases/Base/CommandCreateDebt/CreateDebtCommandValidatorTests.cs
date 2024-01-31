using AutoFixture;
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

    private DateTimeOffset Begin => DateTimeOffset.UtcNow;
    private DateTimeOffset End => Begin + new TimeSpan(1, 0, 0);

    [Fact]
    public async Task Begin_EqualToEnd_GreaterThanErrorMessage()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Body = command.Body with
            {
                End = command.Body.Begin
            }
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveGreaterThanError(x => x.Body.End, x => x.Body.Begin);
    }

    [Fact]
    public async Task Begin_GreaterThanNow_LessThanOrEqualToErrorMessage()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Body = command.Body with
            {
                Begin = command.Body.End,
                End = null
            }
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveLessThanOrEqualToError(x => x.Body.Begin, Begin.AddMinutes(5));
    }

    [Fact]
    public async Task End_Null_Ok()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Body = command.Body with
            {
                End = null
            }
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task Login_Empty_EmptyErrorMessage()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Body = command.Body with
            {
                Login = null!
            }
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveNotEmptyError(x => x.Body.Login);
    }

    [Fact]
    public async Task Login_GreaterThanMax_MaximumLengthErrorMessage()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Body = command.Body with
            {
                Login = TestHelper.String(LoginMaxLength + 1)
            }
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveMaximumLengthError(x => x.Body.Login, LoginMaxLength);
    }

    [Fact]
    public async Task Source_Empty_EmptyErrorMessage()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Login = null!
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveNotEmptyError(x => x.Login);
    }

    [Fact]
    public async Task Source_GreaterThanMax_MaximumLengthErrorMessage()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Login = TestHelper.String(LoginMaxLength + 1)
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveMaximumLengthError(x => x.Login, LoginMaxLength);
    }

    [Fact]
    public async Task Sum_LessThanMin_GreaterThanErrorMessage()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();
        command = command with
        {
            Body = command.Body with
            {
                Sum = SumMin - 1
            }
        };

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldHaveGreaterThanError(x => x.Body.Sum, SumMin - 1);
    }

    [Fact]
    public async Task Validate_Ok_Ok()
    {
        // Arrange
        var validator = CreateValidator();
        var command = GetCorrectObjectToTest();

        // Act
        var result = await validator.TestValidateAsync(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    private CreateDebtCommandValidator<CreateDebtCommand> CreateValidator()
        => new CreateDebtCommandValidator<CreateDebtCommand>();

    private CreateDebtCommand GetCorrectObjectToTest()
    {
        var fixture = new Fixture();
        var randomLoginString = new RandomString(0, LoginMaxLength);
        fixture.Customizations.Add(new StringGenerator(() => randomLoginString));

        var login = fixture.Create<string>();
        var sum = fixture.Create<int>();
        var body = new CreateDebtCommandBody(login, sum, Begin, End);

        return new CreateDebtCommand()
        {
            Login = fixture.Create<string>(),
            Body = body,
        };
    }
}