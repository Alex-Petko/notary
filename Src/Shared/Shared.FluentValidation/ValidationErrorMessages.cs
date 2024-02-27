using System.Linq.Expressions;
using System.Linq;

namespace Shared.FluentValidation;

public static class ValidationErrorMessages
{
    #region NotEmpty

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberAccessor"></param>
    /// <returns>"The {memberName} cannot be empty"</returns>
    public static string NotEmpty<T, TProperty>(
        Expression<Func<T, TProperty>> memberAccessor)
        => NotEmpty(GetMemberName(memberAccessor));

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
    /// <param name="memberAccessor"></param>
    /// <param name="maximumLength"></param>
    /// <returns>"The {memberName} length must not exceed {maximumLength}"</returns>
    public static string MaximumLength<T>(
        Expression<Func<T, string>> memberAccessor,
        int maximumLength)
        => MaximumLength(GetMemberName(memberAccessor), maximumLength);

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
    /// <param name="memberAccessor"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than {valueToCompare}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(memberAccessor), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberAccessor"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than {valueToCompare}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty?>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(memberAccessor), valueToCompare);

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
    /// <param name="memberAccessor"></param>
    /// <param name="comparedMemberAccessor"></param>
    /// <returns>"The {memberName} must be greater than the {comparedMemberAccessor.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty>> memberAccessor,
        Expression<Func<T, TProperty>> comparedMemberAccessor)
        where TProperty : IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(memberAccessor), comparedMemberAccessor);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberAccessor"></param>
    /// <param name="comparedMemberAccessor"></param>
    /// <returns>"The {memberName} must be greater than the {comparedMemberAccessor.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty?>> memberAccessor,
        Expression<Func<T, TProperty>> comparedMemberAccessor)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(memberAccessor), comparedMemberAccessor);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberAccessor"></param>
    /// <param name="comparedMemberAccessor"></param>
    /// <returns>"The {memberName} must be greater than the {comparedMemberAccessor.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        Expression<Func<T, TProperty?>> memberAccessor,
        Expression<Func<T, TProperty?>> comparedMemberAccessor)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => GreaterThan(GetMemberName(memberAccessor), comparedMemberAccessor);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="comparedMemberAccessor"></param>
    /// <returns>"The {memberName} must be greater than the {comparedMemberAccessor.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        string memberName,
        Expression<Func<T, TProperty>> comparedMemberAccessor)
        where TProperty : IComparable<TProperty>, IComparable
        => $"The {memberName} must be greater than the {GetMemberName(comparedMemberAccessor)}";

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="comparedMemberAccessor"></param>
    /// <returns>"The {memberName} must be greater than the {comparedMemberAccessor.Member.Name}"</returns>
    public static string GreaterThan<T, TProperty>(
        string memberName,
        Expression<Func<T, TProperty?>> comparedMemberAccessor)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => $"The {memberName} must be greater than the {GetMemberName(comparedMemberAccessor)}";

    #endregion GreaterThan

    #region GreaterThanOEqualTo

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberAccessor"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be greater than or equal to {valueToCompare}"</returns>
    public static string GreaterThanOEqualTo<T, TProperty>(
        Expression<Func<T, TProperty>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => GreaterThanOrEqualTo(GetMemberName(memberAccessor), valueToCompare);

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
    /// <param name="memberAccessor"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than {valueToCompare}"</returns>
    public static string LessThan<T, TProperty>(
        Expression<Func<T, TProperty>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => LessThan(GetMemberName(memberAccessor), valueToCompare);

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
    /// <param name="memberAccessor"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than or equal to {valueToCompare}"</returns>
    public static string LessThanOrEqualTo<T, TProperty>(
        Expression<Func<T, TProperty>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : IComparable<TProperty>, IComparable
        => LessThanOrEqualTo(GetMemberName(memberAccessor), valueToCompare);

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberAccessor"></param>
    /// <param name="valueToCompare"></param>
    /// <returns>"The {memberName} must be less than or equal to {valueToCompare}"</returns>
    public static string LessThanOrEqualTo<T, TProperty>(
        Expression<Func<T, TProperty?>> memberAccessor,
        TProperty valueToCompare)
        where TProperty : struct, IComparable<TProperty>, IComparable
        => LessThanOrEqualTo(GetMemberName(memberAccessor), valueToCompare);

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

    #region EqualToAny
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberAccessor"></param>
    /// <param name="values"></param>
    /// <returns>"The {memberName} must be any of [{string.Join(", ", values)}]"</returns>
    public static string EqualToAny<T, TProperty>(
        Expression<Func<T, TProperty>> memberAccessor,
        IEnumerable<TProperty> values)
        where TProperty : IComparable<TProperty>, IComparable
        => EqualToAny(GetMemberName(memberAccessor), values);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TProperty"></typeparam>
    /// <param name="memberName"></param>
    /// <param name="values"></param>
    /// <returns>"The {memberName} must be any of [{string.Join(", ", values)}]"</returns>
    public static string EqualToAny<TProperty>(
        string memberName,
        IEnumerable<TProperty> values)
        where TProperty : IComparable<TProperty>, IComparable
        => $"The {memberName} must be any of [{string.Join(", ", values)}]";
    #endregion

    private static string GetMemberName<T, TProperty>(Expression<Func<T, TProperty>> memberAccessor)
        => ((MemberExpression)memberAccessor.Body).Member.Name;
}