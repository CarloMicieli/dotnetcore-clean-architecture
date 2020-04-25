using AutoMapper;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.Boundaries.Catalog.CreateScale;
using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateRailway;
using TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale;
using TreniniDotNet.Web.UseCases.V1.Catalog.EditBrand;

namespace TreniniDotNet.Web.UseCases.V1.Catalog
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateBrandRequest, CreateBrandInput>();
            CreateMap<EditBrandRequest, EditBrandInput>();

            CreateMap<CreateRailwayRequest, CreateRailwayInput>();
            CreateMap<TotalRailwayLengthRequest, TotalRailwayLengthInput>();
            CreateMap<RailwayGaugeRequest, RailwayGaugeInput>();
            CreateMap<PeriodOfActivityRequest, PeriodOfActivityInput>();

            CreateMap<CreateScaleRequest, CreateScaleInput>();
            CreateMap<ScaleGaugeRequest, ScaleGaugeInput>();

            CreateMap<RollingStockRequest, RollingStockInput>();
            CreateMap<LengthOverBufferRequest, LengthOverBufferInput>();
            CreateMap<CreateCatalogItemRequest, CreateCatalogItemInput>();
        }
    }
}