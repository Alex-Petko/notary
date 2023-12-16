using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DealProject.Attributes;

public class ClaimValueProviderFactory : IValueProviderFactory
{
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        var bindingSource = BindingSourceResources.Claim;
        var claimsPrincipal = context.ActionContext.HttpContext.User;
        context.ValueProviders.Add(new ClaimValueProvider(bindingSource, claimsPrincipal));

        return Task.CompletedTask;
    }
}