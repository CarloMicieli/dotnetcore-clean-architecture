using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.TestHelpers.SeedData.Collecting;

namespace TreniniDotNet.IntegrationTests.Helpers.Data
{
    public static class ApplicationContextSeed
    {
        public static async Task SeedCatalog(IServiceProvider scopedServices)
        {
            var brands = scopedServices.GetRequiredService<IBrandsRepository>();
            await brands.SeedDatabase();

            var railways = scopedServices.GetRequiredService<IRailwaysRepository>();
            await railways.SeedDatabase();

            var scales = scopedServices.GetRequiredService<IScalesRepository>();
            await scales.SeedDatabase();

            var catalogItems = scopedServices.GetRequiredService<ICatalogItemsRepository>();
            await catalogItems.SeedDatabase();
        }

        public static async Task SeedCollections(IServiceProvider scopedServices)
        {
            var shops = scopedServices.GetRequiredService<IShopsRepository>();
            await shops.SeedDatabase();

            var collections = scopedServices.GetRequiredService<ICollectionsRepository>();
            await collections.SeedDatabase();

            var wishLists = scopedServices.GetRequiredService<IWishlistsRepository>();
            await wishLists.SeedDatabase();
        }
    }
}
