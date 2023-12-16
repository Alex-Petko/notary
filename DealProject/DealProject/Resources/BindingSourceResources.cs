using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DealProject;

public static class BindingSourceResources
{
    public static readonly BindingSource Claim = new (
        id: "Claim", 
        displayName: "BindingSource_Claim",
        isGreedy: false,
        isFromRequest: true);
}
