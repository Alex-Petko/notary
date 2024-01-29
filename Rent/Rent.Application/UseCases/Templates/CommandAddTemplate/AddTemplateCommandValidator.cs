using FluentValidation;

namespace Rent.Application;

internal sealed class AddTemplateCommandValidator : AbstractValidator<AddTemplateCommand>
{
    public AddTemplateCommandValidator()
    {
        RuleFor(x => x.FileStream)
            .SetValidator(new IFileValidator())
            .When(x => x.FileStream is not null);
    }
}
