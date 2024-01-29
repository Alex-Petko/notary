using FluentValidation;

namespace Rent.Application;

internal sealed class GetTemplateQueryValidator : AbstractValidator<GetTemplateQuery>
{
    public GetTemplateQueryValidator()
    {
    }
}
