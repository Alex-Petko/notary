using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Shared.Attributes;

public class ClaimValueProvider : BindingSourceValueProvider
{
    private readonly ClaimsPrincipal _claimsPrincipal;

    public ClaimValueProvider(BindingSource bindingSource, ClaimsPrincipal claimsPrincipal) : base(bindingSource)
    {
        _claimsPrincipal = claimsPrincipal;
    }

    public override bool ContainsPrefix(string prefix)
        => _claimsPrincipal.HasClaim(claim => claim.Type == prefix);

    public override ValueProviderResult GetValue(string key)
    {
        var value = _claimsPrincipal.FindFirstValue(key);
        return value != null ? new ValueProviderResult(value) : ValueProviderResult.None;
    }
}