using System.Linq.Expressions;

namespace Shared.FluentValidation.Extensions;

public static class ValidationErrorMessages
{
    #region NotEmpty

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <returns>"The {memberName} cannot be empty"</returns>
    public static string NotEmpty<T, TProperty>(
        Expression<Func<T, TProperty>> expression)
        => NotEmpty(GetMemberName(expression));

    /// <summary>
    ///
    /// </summary>
    /// <param name="memberName"></param>
    /// <returns>"The {memberName} cannot be empty"</returns>
    public static string NotEmpty(
        string memberName)
        => $"The {memberName} cannot be empty";

    #endregion NotEmpty

    #region MaximumLength

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="maximumLength"></param>
    /// <returns>"The {memberName} length must not exceed {maximumLength}"</returns>
    public static string MaximumLength<T>(
        Expression<Func<T, string>> expression,
        int maximumLength)
        => MaximumLength(GetMemberName(expression), maximumLength);

    /// <summary>
    ///
    /// </summary>
    /// <param name="memberName"></param>
    /// <param name="maximumLength"></param>
    /// <returns>"The {memberName} length must not exceed {maximumLength}"</returns>
    public static string MaximumLength(
        string memberName,
        int maximumLength)
        => $"The {memberName} length must not exceed {maximumLength}";

    #endregion MaximumLength

    #region GreaterThan

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than {valueToCompare}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty>> expression,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(expression), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than {valueToCompare}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty?>> expression,
        TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(expression), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than {valueToCompare}"</returns>
    public static string GreaterThan<TProperty>(
        string memberName,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => $"The {memberName} must be greater than {valueToCompare}";

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="expression2"></param>
    /// <returns>"The {memberName} must be greater than the {expression2.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty>> expression,
        Expression<Func<T, TProperty>> expression2)
        where TProperty : IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(expression), expression2);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="expression2"></param>
    /// <returns>"The {memberName} must be greater than the {expression2.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty?>> expression,
        Expression<Func<T, TProperty>> expression2)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(expression), expression2);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="expression2"></param>
    /// <returns>"The {memberName} must be greater than the {expression2.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty?>> expression,
        Expression<Func<T, TProperty?>> expression2)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(expression), expression2);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="expression2"></param>
    /// <returns>"The {memberName} must be greater than the {expression2.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        string memberName,
        Expression<Func<T, TProperty>> expression2)
        where TProperty : IComparable<TProperty>, IComparable
        => $"The {memberName} must be greater than the {GetMemberName(expression2)}";

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="expression2"></param>
    /// <returns>"The {memberName} must be greater than the {expression2.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        string memberName,
        Expression<Func<T, TProperty?>> expression2)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => $"The {memberName} must be greater than the {GetMemberName(expression2)}";

    #endregion GreaterThan

    #region GreaterThanOEqualTo

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than or equal to {valueToCompare}"</returns>
    public static string GreaterThanOEqualTo<T, TProperty>(
        Expression<Func<T, TProperty>> expression,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => GreaterThanOrEqualTo(GetMemberName(expression), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than or equal to {valueToCompare}"</returns>
    public static string GreaterThanOrEqualTo<TProperty>(
        string memberName,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => $"The {memberName} must be greater than or equal to {valueToCompare}";

    #endregion GreaterThanOEqualTo

    #region LessThan

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than {valueToCompare}"</returns>
    public static string LessThan<T, TProperty>(
        Expression<Func<T, TProperty>> expression,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => LessThan(GetMemberName(expression), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than {valueToCompare}"</returns>
    public static string LessThan<TProperty>(
        string memberName,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => $"The {memberName} must be less than to {valueToCompare}";

    #endregion LessThan

    #region LessThanOrEqualTo

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than or equal to {valueToCompare}"</returns>
    public static string LessThanOrEqualTo<T, TProperty>(
        Expression<Func<T, TProperty>> expression,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => LessThanOrEqualTo(GetMemberName(expression), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="expression"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than or equal to {valueToCompare}"</returns>
    public static string LessThanOrEqualTo<T, TProperty>(
        Expression<Func<T, TProperty?>> expression,
        TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => LessThanOrEqualTo(GetMemberName(expression), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than or equal to {valueToCompare}"</returns>
    public static string LessThanOrEqualTo<TProperty>(
        string memberName,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => $"The {memberName} must be less than or equal to {valueToCompare}";

    #endregion LessThanOrEqualTo

    private static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> expression)
        => ((MemberExpression)expression.Body).Member.Name;
}