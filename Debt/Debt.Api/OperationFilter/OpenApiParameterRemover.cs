using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Debt.Api;

public class OpenApiParameterRemover<T> : IOperationFilter
    where T : Attribute
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parameters = context
            .ApiDescription
            .ParameterDescriptions
            .Where(ParameterHasIgnoreAttribute)
            .Join(operation.Parameters, x => x.Name, x => x.Name, (_, x) => x);

        foreach (var parameter in parameters)
            operation.Parameters.Remove(parameter);
    }

    private static bool ParameterHasIgnoreAttribute(ApiParameterDescription parameterDescription)
    {
        if (parameterDescription.ModelMetadata is not DefaultModelMetadata metadata)
            return false;

        return metadata
            .Attributes
            .Attributes
            .Union(metadata.Attributes.ParameterAttributes ?? Array.Empty<object>())
            .Union(metadata.Attributes.PropertyAttributes ?? Array.Empty<object>())
            .Any(x => x is T);
    }
}
