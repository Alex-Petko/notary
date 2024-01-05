namespace Shared.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public sealed class FromSubClaimAttribute : FromClaimAttribute
{
    public FromSubClaimAttribute() : base("sub")
    {
    }
}