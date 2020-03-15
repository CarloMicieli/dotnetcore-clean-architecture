using System.Collections.Generic;
using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemRequest : IRequest
    {
        public string? BrandName { set; get; }

        public string? ItemNumber { set; get; }

        public string? Description { set; get; }

        public string? PrototypeDescription { set; get; }

        public string? ModelDescription { set; get; }

        public string? PowerMethod { set; get; }

        public string? Scale { set; get; }

        public List<RollingStock> RollingStocks { set; get; } = new List<RollingStock>();
    }

    public sealed class RollingStock 
    {
        public string? Era { set; get; }
        
        public decimal? Length { set; get; }
        
        public string? Railway { set; get; }
        
        public string? ClassName { set; get; }
        
        public string? RoadNumber { set; get; }

        public string? Category { set; get; }
    }
}