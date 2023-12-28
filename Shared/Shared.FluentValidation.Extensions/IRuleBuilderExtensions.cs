using FluentValidation;
using System.Linq.Expressions;

namespace Shared.FluentValidation.Extensions;

public static class IRuleBuilderExtensions
{
    /// <summary>
	/// Defines a 'not empty' validator on the current rule builder.
    /// Adds a default error message.
	/// Validation will fail if the property is null, an empty string, whitespace, an empty collection or the default value for the type (for example, 0 for integers but null for nullable integers)
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.NotEmpty(memberName));
    }

    /// <summary>
	/// Defines a length validator on the current rule builder, but only for string properties.
    /// Adds a default error message.
	/// Validation will fail if the length of the string is larger than the length specified.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <param name="maximumLength"></param>
	/// <returns></returns>
    public static IRuleBuilderOptions<T, string> MaximumLengthWithMessage<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .MaximumLength(maximumLength)
            .WithMessage(ValidationErrorMessages.MaximumLength(memberName, maximumLength));
    }

    /// <summary>
	/// Defines a 'greater than' validator on the current rule builder using a lambda expression.
    /// Adds a default error message.
	/// The validation will succeed if the property value is greater than the specified value.
	/// The validation will fail if the property value is less than or equal to the specified value.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <param name="expression">The value being compared</param>
    public static IRuleBuilderOptions<T, TProperty> GreaterThanWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, TProperty>> expression)
        where TProperty : IComparable<TProperty>, IComparable
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .GreaterThan(expression)
            .WithMessage(ValidationErrorMessages.GreaterThan(memberName, expression));
    }

    /// <summary>
	/// Defines a 'greater than' validator on the current rule builder using a lambda expression.
    /// Adds a default error message.
	/// The validation will succeed if the property value is greater than the specified value.
	/// The validation will fail if the property value is less than or equal to the specified value.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <param name="expression">The value being compared</param>
    public static IRuleBuilderOptions<T, TProperty?> GreaterThanWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder,
        Expression<Func<T, TProperty>> expression)
        where TProperty : struct, IComparable<TProperty>, IComparable
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .GreaterThan(expression)
            .WithMessage(ValidationErrorMessages.GreaterThan(memberName, expression));
    }

    /// <summary>
    /// Defines a 'greater than' validator on the current rule builder using a lambda expression.
    /// Adds a default error message.
    /// The validation will succeed if the property value is greater than the specified value.
    /// The validation will fail if the property value is less than or equal to the specified value.
    /// </summary>
    /// <typeparam name="T">Type of object being validated</typeparam>
    /// <typeparam name="TProperty">Type of property being validated</typeparam>
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
    /// <param name="valueToCompare">The value being compared</param>
    public static IRuleBuilderOptions<T, TProperty> GreaterThanWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .GreaterThan(valueToCompare)
            .WithMessage(ValidationErrorMessages.GreaterThan(memberName, valueToCompare));
    }

    /// <summary>
	/// Defines a 'greater than or equal' validator on the current rule builder.
    /// Adds a default error message.
	/// The validation will succeed if the property value is greater than or equal the specified value.
	/// The validation will fail if the property value is less than the specified value.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <param name="valueToCompare">The value being compared</param>
	/// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> GreaterThanOrEqualToWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .GreaterThanOrEqualTo(valueToCompare)
            .WithMessage(ValidationErrorMessages.GreaterThanOrEqualTo(memberName, valueToCompare));
    }

    /// <summary>
	/// Defines a 'less than' validator on the current rule builder.
    /// Adds a default error message.
	/// The validation will succeed if the property value is less than the specified value.
	/// The validation will fail if the property value is greater than or equal to the specified value.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <param name="valueToCompare">The value being compared</param>
	/// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> LessThanWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .LessThan(valueToCompare)
            .WithMessage(ValidationErrorMessages.LessThan(memberName, valueToCompare));
    }

    /// <summary>
	/// Defines a 'less than or equal' validator on the current rule builder.
    /// Adds a default error message.
	/// The validation will succeed if the property value is less than or equal to the specified value.
	/// The validation will fail if the property value is greater than the specified value.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <param name="valueToCompare">The value being compared</param>
	/// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> LessThanOrEqualToWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
    {
        string memberName = GetMemberName(ruleBuilder);

        return ruleBuilder
            .LessThanOrEqualTo(valueToCompare)
            .WithMessage(ValidationErrorMessages.LessThanOrEqualTo(memberName, valueToCompare));
    }

    private static string GetMemberName<T, TProperty>(IRuleBuilder<T, TProperty> builder)
    {
        string memberName = null!;

        // Or first or second
        // IRuleBuilder is always one of them
        (builder as IRuleBuilderInitial<T, TProperty>)?.Configure(rule => memberName = GetMemberName(rule.Expression));
        (builder as IRuleBuilderOptions<T, TProperty>)?.Configure(rule => memberName = GetMemberName(rule.Expression));

        return memberName;
    }

    private static string GetMemberName(LambdaExpression expression) => ((MemberExpression)expression.Body).Member.Name;
}