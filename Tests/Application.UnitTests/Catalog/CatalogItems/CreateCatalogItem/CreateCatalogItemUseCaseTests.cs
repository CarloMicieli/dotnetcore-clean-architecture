using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Application.InMemory.Catalog.CatalogItems.OutputPorts;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.TestInputs.Catalog;
using TreniniDotNet.Application.UseCases;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.Common.Uuid.Testing;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using Xunit;

namespace TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem
{
    public class CreateCatalogItemUseCaseTests : CatalogUseCaseTests<CreateCatalogItemUseCase, CreateCatalogItemOutput, CreateCatalogItemOutputPort>
    {
        [Fact]
        public async Task CreateCatalogItem_ShouldValidateInput()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brand: "",
                itemNumber: "",
                description: null,
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "not valid",
                scale: null,
                deliveryDate: null, available: false,
                rollingStocks: EmptyRollingStocks());

            await useCase.Execute(input);

            outputPort.ShouldHaveValidationErrors();
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheBrandExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.Empty, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brand: "not found",
                itemNumber: "12345",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "h0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("III", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.AssertBrandWasNotFound(Slug.Of("not found"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheCatalogItemDoesNotExistAlready()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brand: "acme",
                itemNumber: "60458",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "h0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertCatalogItemAlreadyExists(CatalogSeedData.Brands.Acme(), new ItemNumber("60458"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheScaleExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brand: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "not exists",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "FS"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertScaleWasNotFound(Slug.Of("not exists"));
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckTheRollingStockRailwayExists()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brand: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList("VI", Category.ElectricLocomotive.ToString(), "not found"));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(new List<Slug>() { Slug.Of("not found") });
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCheckThat_AllRollingStockRailwaysExist()
        {
            var (useCase, outputPort) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brand: "acme",
                itemNumber: "ZZZZZZ",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList(
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "not found1"),
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "not found2")));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.AssertRailwayWasNotFound(new List<Slug>() { Slug.Of("not found1"), Slug.Of("not found2") });
        }

        [Fact]
        public async Task CreateCatalogItem_ShouldCreateANewCatalogItem()
        {
            var (useCase, outputPort, unitOfWork) = ArrangeCatalogItemUseCase(Start.WithSeedData, NewCreateCatalogItem);

            var input = new CreateCatalogItemInput(
                brand: "acme",
                itemNumber: "99999",
                description: "My new catalog item",
                prototypeDescription: null,
                modelDescription: null,
                powerMethod: "dc",
                scale: "H0",
                deliveryDate: null, available: false,
                rollingStocks: RollingStockList(
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "fs"),
                    RollingStock("VI", Category.ElectricLocomotive.ToString(), "fs")));

            await useCase.Execute(input);

            outputPort.ShouldHaveNoValidationError();
            outputPort.ShouldHaveStandardOutput();

            unitOfWork.EnsureUnitOfWorkWasSaved();

            Assert.Equal(Slug.Of("acme-99999"), outputPort.UseCaseOutput.Slug);
            Assert.NotEqual(CatalogItemId.Empty, outputPort.UseCaseOutput.Id);
        }

        private CreateCatalogItemUseCase NewCreateCatalogItem(CatalogItemService catalogItemService, CreateCatalogItemOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            var fakeClock = new FakeClock(Instant.FromUtc(1988, 11, 25, 0, 0));

            ICatalogItemsFactory catalogItemsFactory = new CatalogItemsFactory(
                fakeClock,
                FakeGuidSource.NewSource(new Guid("3d02506b-8263-4e14-880d-3f3caf22c562")));

            IRollingStocksFactory rollingStocksFactory = new RollingStocksFactory(
                fakeClock, FakeGuidSource.NewSource(Guid.NewGuid()));

            return new CreateCatalogItemUseCase(outputPort,
                catalogItemService, rollingStocksFactory,
                unitOfWork);
        }

        private IReadOnlyList<RollingStockInput> EmptyRollingStocks()
        {
            return new List<RollingStockInput>();
        }

        private static IReadOnlyList<RollingStockInput> RollingStockList(string era, string category, string railway)
        {
            var rollingStockInput = CatalogInputs.NewRollingStockInput.With(
                    era: era,
                    category: category,
                    railway: railway);
            return new List<RollingStockInput>() { rollingStockInput };
        }

        private static RollingStockInput RollingStock(string era, string category, string railway)
        {
            return CatalogInputs.NewRollingStockInput.With(
                    era: era,
                    category: category,
                    railway: railway,
                    length: new LengthOverBufferInput(999M, null));
        }

        private static IReadOnlyList<RollingStockInput> RollingStockList(params RollingStockInput[] inputs)
        {
            return inputs.ToList();
        }
    }
}