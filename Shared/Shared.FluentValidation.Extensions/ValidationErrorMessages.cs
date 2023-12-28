using System.Linq.Expressions;

namespace Shared.FluentValidation.Extensions;

public static class ValidationErrorMessages
{
    /// <summary>
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns>"The {memberName} cannot be empty"</returns>
    public static string NotEmpty<T, TProperty>(Expression<Func<T, TProperty>> expression)
        => NotEmpty(GetMemberName(expression));

    /// <summary>
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns>"The {memberName} cannot be empty"</returns>
    public static string NotEmpty(string memberName)
        => $"The {memberName} cannot be empty";

    /// <summary>
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns>"The {memberName} length must not exceed {maximumLength}"</returns>
    public static string MaximumLength<T, TProperty>(Expression<Func<T, TProperty>> expression, int maximumLength)
        => MaximumLength(GetMemberName(expression), maximumLength);

    /// <summary>
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns>"The {memberName} length must not exceed {maximumLength}""</returns>
    public static string MaximumLength(string memberName, int maximumLength)
        => $"The {memberName} length must not exceed {maximumLength}";

    private static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        => ((MemberExpression)expression.Body).Member.Name;
}