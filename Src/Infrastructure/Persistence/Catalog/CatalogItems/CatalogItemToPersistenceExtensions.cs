using EfCatalogItem = TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.CatalogItem;
using DomainCatalogItem = TreniniDotNet.Domain.Catalog.CatalogItems.CatalogItem;
using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
    public static class CatalogItemToPersistenceExtensions
    {
        public static EfCatalogItem ToPersistence(this DomainCatalogItem catalogItem, ApplicationDbContext context)
        {
            var brand = new Brand
            {
                BrandId = catalogItem.Brand.BrandId.ToGuid()
            };
            context.Brands.Add(brand);
            context.Entry<Brand>(brand).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            
            var scale = new Scale
            {
                ScaleId = catalogItem.Scale.ScaleId.ToGuid()
            };
            context.Scales.Add(scale);
            context.Entry<Scale>(scale).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            var railway = new Railway { RailwayId = catalogItem.RollingStocks[0].Railway.RailwayId.ToGuid() };
            context.Railways.Add(railway);
            context.Entry<Railway>(railway).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

            IEnumerable<RollingStock> rollingStocks = catalogItem.RollingStocks
                .Select(rs => new RollingStock {
                    RollingStockId = new Guid(),
                    Category = rs.Category.ToString(),
                    Era = rs.Era.ToString(),
                    Length = (decimal) rs.Length.ToMillimeters(),
                    Railway = railway, //TODO: fixme
                    ClassName = rs.ClassName,
                    RoadNumber = rs.RoadNumber
                });

            return new EfCatalogItem
            {
                CatalogItemId = catalogItem.CatalogItemId.ToGuid(),
                Brand = brand,
                ItemNumber = catalogItem.ItemNumber.ToString(),
                PowerMethod = catalogItem.PowerMethod.ToString(),
                Slug = catalogItem.Slug.ToString(),
                Scale = scale,
                ModelDescription = catalogItem.ModelDescription,
                PrototypeDescription = catalogItem.PrototypeDescription,
                Description = catalogItem.Description,
                RollingStocks = rollingStocks.ToList()
            };
        }
    }
}