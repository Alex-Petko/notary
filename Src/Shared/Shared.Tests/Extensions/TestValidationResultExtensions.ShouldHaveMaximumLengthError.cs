using FluentValidation.TestHelper;
using Shared.FluentValidation;
using System.Linq.Expressions;

namespace Shared.Tests;

public static partial class TestValidationResultExtensions
{
    public static ITestValidationWith ShouldHaveMaximumLengthError<T>(
        this TestValidationResult<T> testValidationResult,
        Expression<Func<T, string>> memberAccessor,
        int maxLength)
    {
        var message = ValidationErrorMessages.MaximumLength(memberAccessor, maxLength);
        return testValidationResult.ShouldHaveValidationErrorFor(memberAccessor)
            .WithErrorMessage(message);
    }
}
