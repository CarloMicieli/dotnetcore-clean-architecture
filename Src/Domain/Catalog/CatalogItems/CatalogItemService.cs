using System;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class CatalogItemService
    {
        private readonly ICatalogItemRepository _catalogItemsRepository;
        private readonly IBrandsRepository _brandsRepository;
        private readonly IScalesRepository _scales;
        private readonly IRailwaysRepository _railways;

        public CatalogItemService(
            ICatalogItemRepository catalogItemsRepository,
            IBrandsRepository brands,
            IScalesRepository scales,
            IRailwaysRepository railways)
        {
            _catalogItemsRepository = catalogItemsRepository;
            _brandsRepository = brands;
            _railways = railways;
            _scales = scales;
        }

        public Task<IBrand?> FindBrandByName(string brandName)
        {
            return _brandsRepository.GetByName(brandName.Trim());
        }

        public async Task<bool> ItemAlreadyExists(IBrand brand, ItemNumber itemNumber)
        {
            var item = await _catalogItemsRepository.GetBy(brand, itemNumber);
            return item != null;
        }

        public Task CreateCatalogItem(
            string brandName,
            ItemNumber itemNumber,
            string description,
            string? modelDescription,
            string? prototypeDescription,
            PowerMethod powerMethod,
            IRollingStock rollingStock)
        {
            throw new NotImplementedException("CreateCatalogItem.TODO");
        }

        public Task<IScale?> FindScaleByName(string scale)
        {
            return _scales.GetByName(scale.Trim());
        }

        public Task<IRailway?> FindRailwayByName(string railwayName)
        {
            return _railways.GetByName(railwayName.Trim());
        }

        public Task CreateCatalogItem(
            string brandName,
            ItemNumber itemNumber,
            string description,
            string? modelDescription,
            string? prototypeDescription,
            PowerMethod powerMethod,
            IRollingStock rollingStock1,
            IRollingStock rollingStock2)
        {
            throw new NotImplementedException("CreateCatalogItem.TODO");
        }

        public Task CreateCatalogItem(
            string brandName,
            ItemNumber itemNumber,
            string description,
            string? modelDescription,
            string? prototypeDescription,
            PowerMethod powerMethod,
            params IRollingStock[] rollingStocks)
        {
            throw new NotImplementedException("CreateCatalogItem.TODO");
        }
    }
}