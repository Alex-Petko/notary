using FluentValidation.TestHelper;
using Shared.FluentValidation;
using System.Linq.Expressions;

namespace Shared.Tests;

public static partial class TestValidationResultExtensions
{
    public static ITestValidationWith ShouldHaveGreaterThanError<T, TProperty>(
        this TestValidationResult<T> testValidationResult,
        Expression<Func<T, TProperty>> memberAccessor,
        Expression<Func<T, TProperty>> comparedMemberAccessor)
        where TProperty : IComparable<TProperty>, IComparable
    {
        var message = ValidationErrorMessages.GreaterThan(memberAccessor, comparedMemberAccessor);
        return testValidationResult.ShouldHaveValidationErrorFor(memberAccessor)
            .WithErrorMessage(message);
    }

    public static ITestValidationWith ShouldHaveGreaterThanError<T, TProperty>(
        this TestValidationResult<T> testValidationResult,
        Expression<Func<T, TProperty?>> memberAccessor,
        Expression<Func<T, TProperty?>> comparedMemberAccessor)
        where TProperty : struct, IComparable<TProperty>, IComparable
    {
        var message = ValidationErrorMessages.GreaterThan(memberAccessor, comparedMemberAccessor);
        return testValidationResult.ShouldHaveValidationErrorFor(memberAccessor)
            .WithErrorMessage(message);
    }

    public static ITestValidationWith ShouldHaveGreaterThanError<T, TProperty>(
        this TestValidationResult<T> testValidationResult,
        Expression<Func<T, TProperty>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
    {
        var message = ValidationErrorMessages.GreaterThan(memberAccessor, valueToCompare);
        return testValidationResult.ShouldHaveValidationErrorFor(memberAccessor)
            .WithErrorMessage(message);
    }

    public static ITestValidationWith ShouldHaveGreaterThanError<T, TProperty>(
        this TestValidationResult<T> testValidationResult,
        Expression<Func<T, TProperty?>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable
    {
        var message = ValidationErrorMessages.GreaterThan(memberAccessor, valueToCompare);
        return testValidationResult.ShouldHaveValidationErrorFor(memberAccessor)
            .WithErrorMessage(message);
    }
}
