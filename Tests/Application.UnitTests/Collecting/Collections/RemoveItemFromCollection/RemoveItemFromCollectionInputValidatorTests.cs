﻿using System;
using FluentValidation.TestHelper;
using TreniniDotNet.Application.TestInputs.Collecting;
using TreniniDotNet.Domain.Collecting.Shared;
using Xunit;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public class RemoveItemFromCollectionInputValidatorTests
    {
        private RemoveItemFromCollectionInputValidator Validator { get; }

        public RemoveItemFromCollectionInputValidatorTests()
        {
            Validator = new RemoveItemFromCollectionInputValidator();
        }


        [Fact]
        public void RemoveItemFromCollectionInput_ShouldSucceedValidation()
        {
            var input = CollectingInputs.RemoveItemFromCollection.With(
                Owner: new Owner("George"),
                Id: Guid.NewGuid(),
                ItemId: Guid.NewGuid());

            var result = Validator.TestValidate(input);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void RemoveItemFromCollectionInput_ShouldFailValidation_WhenEmpty()
        {
            var input = CollectingInputs.RemoveItemFromCollection.Empty;

            var result = Validator.TestValidate(input);

            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ItemId);
            result.ShouldHaveValidationErrorFor(x => x.Owner);
        }
    }
}