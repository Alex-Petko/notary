namespace Shared.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class SwaggerIgnoreAttribute : Attribute
{
}