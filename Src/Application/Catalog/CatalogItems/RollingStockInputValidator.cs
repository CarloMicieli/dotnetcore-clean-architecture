using FluentValidation;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public sealed class RollingStockInputValidator : AbstractValidator<RollingStockInput>
    {
        public RollingStockInputValidator()
        {
            RuleFor(x => x.Epoch)
                .NotNull()
                .ValidEpoch();

            RuleFor(x => x.Category)
                .NotNull()
                .IsEnumName(typeof(Category), caseSensitive: false);

            RuleFor(x => x.LengthOverBuffer)
                .SetValidator(new LengthOverBufferInputValidator());

            RuleFor(x => x.Railway)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(25);

            RuleFor(x => x.RoadNumber)
                .MaximumLength(25);

            RuleFor(x => x.ClassName)
                .MaximumLength(25);

            RuleFor(x => x.DccInterface)
                .IsEnumName(typeof(DccInterface), caseSensitive: false);

            RuleFor(x => x.Control)
                .IsEnumName(typeof(Control), caseSensitive: false);
        }
    }
}
