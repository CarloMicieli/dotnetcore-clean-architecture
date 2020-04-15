﻿using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateCollection
{
    public sealed class CreateCollectionInputValidator : AbstractValidator<CreateCollectionInput>
    {
        public CreateCollectionInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Notes)
                .MaximumLength(150);
        }
    }
}
