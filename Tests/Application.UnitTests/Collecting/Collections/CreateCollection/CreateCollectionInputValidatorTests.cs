using FluentValidation.TestHelper;
using TreniniDotNet.TestHelpers.Common;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public class CreateCollectionInputValidatorTests
    {
        private CreateCollectionInputValidator Validator { get; }

        public CreateCollectionInputValidatorTests()
        {
            Validator = new CreateCollectionInputValidator();
        }

        [Fact]
        public void CreateCollectionInput_ShouldSucceedValidation()
        {
            var input = NewCreateCollectionInput.With(owner: "George", notes: "My notes");

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateCollectionInput_ShouldFailValidation_WhenEmpty()
        {
            var input = NewCreateCollectionInput.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }

        [Fact]
        public void CreateCollectionInput_ShouldFailValidation_WhenNotesAreTooLong()
        {
            var input = NewCreateCollectionInput.With(
                notes: RandomString.WithLengthOf(151));

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Notes);
        }
    }
}