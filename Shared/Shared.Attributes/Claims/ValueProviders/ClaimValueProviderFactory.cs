using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.Attributes;

public sealed class ClaimValueProviderFactory : IValueProviderFactory
{
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        var bindingSource = CustomBindingSource.Claim;
        var claimsPrincipal = context.ActionContext.HttpContext.User;
        context.ValueProviders.Add(new ClaimValueProvider(bindingSource, claimsPrincipal));

        return Task.CompletedTask;
    }
}