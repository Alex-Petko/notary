using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Shared.Attributes;

public static class CustomBindingSource
{
    public static readonly BindingSource Claim = new(
        id: nameof(Claim),
        displayName: nameof(CustomBindingSource) + "_" + nameof(Claim),
        isGreedy: false,
        isFromRequest: true);
}
