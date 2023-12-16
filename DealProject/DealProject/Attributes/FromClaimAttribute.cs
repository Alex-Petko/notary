using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DealProject.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class FromClaimAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider
{
    public BindingSource BindingSource => BindingSourceResources.Claim;

    public string? Name { get; }

    public FromClaimAttribute(string type)
    {
        Name = type;
    }
}
