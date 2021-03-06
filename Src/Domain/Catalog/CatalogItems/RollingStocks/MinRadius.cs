using System;
using TreniniDotNet.SharedKernel.Lengths;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    /// <summary>
    /// Minimum curve radius for rolling stocks, in millimeters.
    /// </summary>
    public sealed class MinRadius : IComparable<MinRadius>
    {
        private MinRadius(Length value)
        {
            Value = value;
        }

        public Length Value { get; }

        public decimal Millimeters => Value.Value;

        public static MinRadius OfMillimeters(decimal value)
        {
            var len = Length.OfMillimeters(value);
            return new MinRadius(len);
        }

        public static MinRadius? CreateOrDefault(decimal? minRadius)
        {
            if (minRadius.HasValue)
            {
                return MinRadius.OfMillimeters(minRadius.Value);
            }

            return null;
        }

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        #region [ Equality / Comparable ]

        public override bool Equals(object? obj)
        {
            if (obj is MinRadius other)
            {
                return this == other;
            }

            return false;
        }

        public static bool operator ==(MinRadius left, MinRadius right) => left?.Value == right?.Value;

        public static bool operator !=(MinRadius left, MinRadius right) => !(left == right);

        public int CompareTo(MinRadius? other)
        {
            if (other is null)
            {
                return 1;
            }

            return this.Value.CompareTo(other.Value);
        }

        #endregion
    }
}
