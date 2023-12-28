using FluentValidation;
using System.Linq.Expressions;

namespace Shared.FluentValidation.Extensions;

public static class IRuleBuilderExtensions
{
    /// <summary>
	/// Defines a 'not empty' validator on the current rule builder. And adds a default error message.
	/// Validation will fail if the property is null, an empty string, whitespace, an empty collection or the default value for the type (for example, 0 for integers but null for nullable integers)
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <typeparam name="TProperty">Type of property being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
    {
        string memberName = GetMemberName(builder);

        return builder
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.NotEmpty(memberName));
    }

    /// <summary>
	/// Defines a length validator on the current rule builder, but only for string properties. And adds a default error message.
	/// Validation will fail if the length of the string is larger than the length specified.
	/// </summary>
	/// <typeparam name="T">Type of object being validated</typeparam>
	/// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
	/// <param name="maximumLength"></param>
	/// <returns></returns>
    public static IRuleBuilderOptions<T, string> MaximumLengthWithMessage<T>(this IRuleBuilder<T, string> builder, int maximumLength)
    {
        string memberName = GetMemberName(builder);

        return builder
            .MaximumLength(maximumLength)
            .WithMessage(ValidationErrorMessages.MaximumLength(memberName, maximumLength));
    }

    private static string GetMemberName<T, TProperty>(IRuleBuilder<T, TProperty> builder)
    {
        string memberName = null!;

        // Or first or second
        // IRuleBuilder is always one of them
        (builder as IRuleBuilderInitial<T, TProperty>)?.Configure(rule => memberName = ((MemberExpression)rule.Expression.Body).Member.Name);
        (builder as IRuleBuilderOptions<T, TProperty>)?.Configure(rule => memberName = ((MemberExpression)rule.Expression.Body).Member.Name);

        return memberName;
    }
}