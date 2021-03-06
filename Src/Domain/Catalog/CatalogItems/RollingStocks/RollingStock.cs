using TreniniDotNet.Common.Domain;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public abstract class RollingStock : Entity<RollingStockId>
    {
        public RailwayRef Railway { get; protected set; } = null!;

        public Category Category { get; protected set; }

        public Epoch Epoch { get; protected set; } = null!;

        public LengthOverBuffer? Length { get; protected set; }

        public MinRadius? MinRadius { get; protected set; }

        public Couplers? Couplers { get; protected set; }

        public string? Livery { get; protected set; }
    }
}
