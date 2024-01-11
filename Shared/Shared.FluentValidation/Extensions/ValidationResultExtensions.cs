using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Shared.FluentValidation;

internal static class ValidationResultExtensions
{
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState, string? prefix)
    {
        if (result.IsValid)
            return;

        foreach (var error in result.Errors)
        {
            var key = string.IsNullOrEmpty(prefix)
                ? error.PropertyName
                : prefix + "." + error.PropertyName;

            if (modelState.ContainsKey(key))
            {
                if (modelState[key] is null)
                    throw new NotImplementedException();

                modelState[key]?.Errors.Add(error.ErrorMessage);
            }
            else
            {
                modelState.AddModelError(key, error.ErrorMessage);
                var valueProviderResult = new ValueProviderResult(error.AttemptedValue?.ToString() ?? "", CultureInfo.CurrentCulture);
                modelState.SetModelValue(key, valueProviderResult);
            }
        }
    }
}