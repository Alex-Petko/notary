﻿using DealProject.Application;
using FluentValidation;

namespace DealProject;

public class LendDebtDtoValidator : AbstractValidator<LendDebtDto>
{
    public LendDebtDtoValidator()
    {

        RuleFor(x => x.Login)
            .NotEmpty();

        RuleFor(x => x.Sum)
            .GreaterThan(0);

        RuleFor(x => x.Begin)
            .LessThanOrEqualTo(DateTime.UtcNow);

        RuleFor(x => x.End)
            .GreaterThan(x => x.Begin)
            .When(x => x.End.HasValue);
    }
}