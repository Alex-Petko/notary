using Microsoft.Extensions.DependencyInjection;

namespace Shared.FluentValidation;

public static class IMvcBuilderExtensions
{
    public static IMvcBuilder AddAutoValidation(this IMvcBuilder builder)
    {
        builder.AddMvcOptions(options => options.Filters.Add<AutoValidationAsyncActionFilter>(-2001));
        return builder;
    }
}
