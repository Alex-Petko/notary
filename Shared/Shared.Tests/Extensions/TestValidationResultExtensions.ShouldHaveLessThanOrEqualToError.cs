using FluentValidation.TestHelper;
using Shared.FluentValidation;
using System.Linq.Expressions;

namespace Shared.Tests;

public static partial class TestValidationResultExtensions
{
    public static ITestValidationWith ShouldHaveLessThanOrEqualToError<T, TProperty>(
        this TestValidationResult<T> testValidationResult,
        Expression<Func<T, TProperty?>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable
    {
        var message = ValidationErrorMessages.LessThanOrEqualTo(memberAccessor, valueToCompare);
        return testValidationResult.ShouldHaveValidationErrorFor(memberAccessor)
            .WithErrorMessage(message);
    }
}
