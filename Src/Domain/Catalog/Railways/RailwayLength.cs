using System;
using TreniniDotNet.Common.Lengths;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwayLength : IEquatable<RailwayLength>
    {
        private static readonly TwoLengths TwoLengths =
            new TwoLengths(MeasureUnit.Kilometers, MeasureUnit.Miles);

        private RailwayLength(Length kilometers, Length miles)
        {
            Kilometers = kilometers;
            Miles = miles;
        }

        public Length Kilometers { get; }
        public Length Miles { get; }

        public static RailwayLength Create(decimal? km, decimal? miles)
        {
            var (lenKm, lenMi) = TwoLengths.Create(km, miles);
            return new RailwayLength(lenKm, lenMi);
        }

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is RailwayLength that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(RailwayLength other) => AreEquals(this, other);

        public static bool operator ==(RailwayLength left, RailwayLength right) => AreEquals(left, right);

        public static bool operator !=(RailwayLength left, RailwayLength right) => !AreEquals(left, right);

        private static bool AreEquals(RailwayLength left, RailwayLength right) =>
            left.Miles == right.Miles && left.Kilometers == right.Kilometers;
        #endregion

        public override int GetHashCode() => HashCode.Combine(Kilometers, Miles);
    }
}