using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.InMemory.Catalog.CatalogItems
{
    public sealed class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly InMemoryContext _context;

        public CatalogItemRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<CatalogItemId> AddAsync(ICatalogItem catalogItem)
        {
            _context.CatalogItems.Add(catalogItem);
            return Task.FromResult(catalogItem.CatalogItemId);
        }

        public Task<bool> ExistsAsync(IBrandInfo brand, ItemNumber itemNumber)
        {
            var exists = _context.CatalogItems
                .Any(it => it?.Brand.BrandId == brand.BrandId && it.ItemNumber == itemNumber);
            return Task.FromResult(exists);
        }

        public Task<ICatalogItem> GetByBrandAndItemNumberAsync(IBrandInfo brand, ItemNumber itemNumber)
        {
            var catalogItem = _context.CatalogItems
                .Where(it => it?.Brand.BrandId == brand.BrandId && it.ItemNumber == itemNumber)
                .FirstOrDefault();

            return Task.FromResult(catalogItem);
        }

        public Task<ICatalogItem> GetBySlugAsync(Slug slug)
        {
            var catalogItem = _context.CatalogItems
                .Where(it => it.Slug == slug)
                .FirstOrDefault();

            return Task.FromResult(catalogItem);
        }

        public Task UpdateAsync(ICatalogItem catalogItem) =>
            Task.CompletedTask;
    }
}