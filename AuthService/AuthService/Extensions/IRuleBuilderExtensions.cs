using FluentValidation;
using FluentValidation.TestHelper;
using System.Data;
using System.Linq.Expressions;

namespace AuthService;

public static class IRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(this IRuleBuilder<T, TProperty> builder)
    {
        string memberName = GetMemberName(builder);

        return builder
            .NotEmpty()
            .WithMessage(ValidationErrorMessages.NotEmpty(memberName));
    }

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
