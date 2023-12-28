using System.Linq.Expressions;

namespace AuthService;

public static class ValidationErrorMessages
{
    public static string NotEmpty<T, TProperty>(Expression<Func<T, TProperty>> expression)
        => NotEmpty(GetMemberName(expression));

    public static string NotEmpty(string memberName) 
        => $"The {memberName} cannot be empty";

    public static string MaximumLength<T, TProperty>(Expression<Func<T, TProperty>> expression, int maximumLength)
        => MaximumLength(GetMemberName(expression), maximumLength);

    public static string MaximumLength(string memberName, int maximumLength)
        => $"The {memberName} length must not exceed {maximumLength}";

    private static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> expression) 
        => ((MemberExpression)expression.Body).Member.Name;
}
