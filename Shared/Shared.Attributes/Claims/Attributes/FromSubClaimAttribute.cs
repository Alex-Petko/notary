namespace Shared.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class FromSubClaimAttribute : FromClaimAttribute
{
    public FromSubClaimAttribute() : base("sub")
    {
    }
}