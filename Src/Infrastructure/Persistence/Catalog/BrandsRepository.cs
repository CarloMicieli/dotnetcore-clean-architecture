using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog
{
    public sealed class BrandsRepository : EfCoreRepository<BrandId, Brand>, IBrandsRepository
    {
        public BrandsRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public Task<bool> ExistsAsync(Slug slug) =>
            DbContext.Brands.AnyAsync(it => it.Slug == slug);

#pragma warning disable 8619
        public Task<Brand?> GetBySlugAsync(Slug slug) =>
            DbContext.Brands.FirstOrDefaultAsync(it => it.Slug == slug);
#pragma warning restore 8619
    }
}
