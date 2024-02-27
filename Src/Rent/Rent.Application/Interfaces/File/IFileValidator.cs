using FluentValidation;
using Shared.FluentValidation;

namespace Rent.Application;

internal sealed class IFileValidator : AbstractValidator<IFile>
{
    public IFileValidator()
    {
        RuleFor(x => x.Name)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(50);

        RuleFor(x => x.FileName)
            .NotEmptyWithMessage()
            .MaximumLengthWithMessage(50);

        RuleFor(x => x.Length)
            .LessThanOrEqualToWithMessage(500_000);

        var validContentType = new HashSet<string>()
        {
            "application/msword"
        };

        RuleFor(x => x.ContentType)
            .EqualToAnyWithMessage(validContentType);
    }
}