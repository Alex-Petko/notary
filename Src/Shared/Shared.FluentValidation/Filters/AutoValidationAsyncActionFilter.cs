using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.FluentValidation;

public class AutoValidationAsyncActionFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public AutoValidationAsyncActionFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var parameter in context.ActionDescriptor.Parameters)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(parameter.ParameterType);
            var validator = _serviceProvider.GetService(validatorType) as IValidator;
            if (validator is null)
                continue;

            var subject = context.ActionArguments[parameter.Name];
            if (subject is null)
                throw new NotImplementedException();

            var validateContext = new ValidationContext<object>(subject);
            var cancelToken = context.HttpContext.RequestAborted;
            var result = await validator.ValidateAsync(validateContext, cancelToken);

            if (result.IsValid)
                continue;

            result.AddToModelState(context.ModelState, null);
        }

        await next();
    }
}