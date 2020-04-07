﻿using FluentValidation.TestHelper;
using Xunit;
using static TreniniDotNet.Application.TestInputs.Catalog.CatalogInputs;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateBrand
{
    public class CreateBrandInputValidatorTests
    {
        private readonly CreateBrandInputValidator validator;

        public CreateBrandInputValidatorTests()
        {
            validator = new CreateBrandInputValidator();
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveNoError_WhenEverythingIsValid()
        {
            var input = NewBrandInput.With(
                Name: "ACME",
                CompanyName: "Associazione Costruzioni Modellistiche Esatte",
                WebsiteUrl: "http://www.acmetreni.com",
                EmailAddress: "mail@acmetreni.com",
                BrandType: "Industrial");

            var result = validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsNull()
        {
            var input = NewBrandInput.Empty();

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenNameIsBlank()
        {
            var input = NewBrandInput.With(Name: "  ");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenEmailIsNotValidMailAddress()
        {
            var input = NewBrandInput.With(EmailAddress: "not a mail");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.EmailAddress);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenWebsiteUrlIsNotValidUri()
        {
            var input = NewBrandInput.With(WebsiteUrl: "not an url");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.WebsiteUrl);
        }

        [Fact]
        public void CreateBrandInputValidator_ShouldHaveError_WhenBrandKindIsNotValid()
        {
            var input = NewBrandInput.With(BrandType: "not a valid type");

            var result = validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Kind);
        }
    }
}
