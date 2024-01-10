namespace Shared.Attributes;

public sealed class FromSubClaimAttribute : FromClaimAttribute
{
    public FromSubClaimAttribute() : base("sub")
    {
    }
}