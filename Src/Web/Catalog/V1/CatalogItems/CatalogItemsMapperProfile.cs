using AutoMapper;
using TreniniDotNet.Application.Catalog.CatalogItems;
using TreniniDotNet.Application.Catalog.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.EditCatalogItem;
using TreniniDotNet.Application.Catalog.CatalogItems.GetCatalogItemBySlug;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.Common.Requests;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.CreateCatalogItem;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.EditCatalogItem;
using TreniniDotNet.Web.Catalog.V1.CatalogItems.GetCatalogItemBySlug;

namespace TreniniDotNet.Web.Catalog.V1.CatalogItems
{
    public class CatalogItemsMapperProfile : Profile
    {
        public CatalogItemsMapperProfile()
        {
            CreateMap<CreateCatalogItemRequest, CreateCatalogItemInput>();
            CreateMap<EditCatalogItemRequest, EditCatalogItemInput>();
            CreateMap<GetCatalogItemBySlugRequest, GetCatalogItemBySlugInput>();
            CreateMap<RollingStockRequest, RollingStockInput>();
            CreateMap<LengthOverBufferRequest, LengthOverBufferInput>();
        }
    }
}