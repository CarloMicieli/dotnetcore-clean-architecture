using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.SeedData.Catalog;

namespace TreniniDotNet.Application.Catalog.CatalogItems.RemoveRollingStockFromCatalogItem
{
    public class RemoveRollingStockFromCatalogItemUseCaseTests : CatalogUseCaseTests<RemoveRollingStockFromCatalogItemUseCase, RemoveRollingStockFromCatalogItemOutput, RemoveRollingStockFromCatalogItemOutputPort>
    {
        [Fact]
        public async Task RemoveRollingStockFromCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewRemoveRollingStockFromCatalogItemUseCase);

            var input = CatalogInputs.NewRemoveRollingStockFromCatalogItemInput.Empty;
            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task RemoveRollingStockFromCatalogItem_ShouldOutputAnErrorWhenRollingStockToDeleteWasNotFound()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewRemoveRollingStockFromCatalogItemUseCase);

            var input = CatalogInputs.NewRemoveRollingStockFromCatalogItemInput.With(
                Slug.Of("not found"),
                RollingStockId.NewId());
            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRollingStockWasNotFound(input.CatalogItemSlug, input.RollingStockId);
        }

        [Fact]
        public async Task RemoveRollingStockFromCatalogItem_ShouldRemoveTheRollingStock()
        {
            var (useCase, outputPort, unitOfWork, dbContext) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewRemoveRollingStockFromCatalogItemUseCase);

            var catalogItem = CatalogSeedData.CatalogItems.Acme_60392();
            var rsId = catalogItem.RollingStocks.First().RollingStockId;

            var input = CatalogInputs.NewRemoveRollingStockFromCatalogItemInput.With(
                catalogItem.Slug,
                rsId);

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();
            outputPort.UseCaseOutput.Should().NotBeNull();

            var modifiedCatalogItem = dbContext.CatalogItems
                .First(it => it.CatalogItemId == catalogItem.CatalogItemId);

            modifiedCatalogItem.RollingStocks
                .All(it => it.RollingStockId != rsId)
                .Should().BeTrue();
        }

        private RemoveRollingStockFromCatalogItemUseCase NewRemoveRollingStockFromCatalogItemUseCase(
            CatalogItemService catalogItemService,
            RemoveRollingStockFromCatalogItemOutputPort outputPort,
            IUnitOfWork unitOfWork)
        {
            return new RemoveRollingStockFromCatalogItemUseCase(outputPort,
                catalogItemService,
                unitOfWork);
        }
    }
}
