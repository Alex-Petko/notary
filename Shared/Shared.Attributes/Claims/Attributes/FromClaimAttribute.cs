using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class FromClaimAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
{
    public BindingSource BindingSource => CustomBindingSource.Claim;

    public string? Name { get; }

    public FromClaimAttribute(string type)
    {
        Name = type;
    }
}
