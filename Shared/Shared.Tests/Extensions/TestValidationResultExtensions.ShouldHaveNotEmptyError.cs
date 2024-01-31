using FluentValidation.TestHelper;
using Shared.FluentValidation;
using System.Linq.Expressions;

namespace Shared.Tests;

public static partial class TestValidationResultExtensions
{
    public static ITestValidationWith ShouldHaveNotEmptyError<T, TProperty>(
        this TestValidationResult<T> testValidationResult,
        Expression<Func<T, TProperty>> memberAccessor)
    {
        var message = ValidationErrorMessages.NotEmpty(memberAccessor);
        return testValidationResult.ShouldHaveValidationErrorFor(memberAccessor)
            .WithErrorMessage(message);
    }
}
