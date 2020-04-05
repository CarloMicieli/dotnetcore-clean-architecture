using System;
using TreniniDotNet.Common.Lengths;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class RailwayGauge : IEquatable<RailwayGauge>
    {
        private static readonly TwoLengths TwoLengths =
            new TwoLengths(MeasureUnit.Inches, MeasureUnit.Millimeters);

        private RailwayGauge(TrackGauge trackGauge, Length inches, Length millimeters)
        {
            TrackGauge = trackGauge;
            Inches = inches;
            Millimeters = millimeters;
        }

        public Length Inches { get; }
        public Length Millimeters { get; }
        public TrackGauge TrackGauge { get; }

        public static RailwayGauge Create(string? trackGauge, decimal? inches, decimal? mm)
        {
            var (lenIn, lenMm) = TwoLengths.Create(inches, mm);
            return new RailwayGauge(trackGauge.ToTrackGauge(), lenIn, lenMm);
        }

        #region [ Equality ]
        public override bool Equals(object obj)
        {
            if (obj is RailwayGauge that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(RailwayGauge other) => AreEquals(this, other);

        public static bool operator ==(RailwayGauge left, RailwayGauge right) => AreEquals(left, right);

        public static bool operator !=(RailwayGauge left, RailwayGauge right) => !AreEquals(left, right);

        private static bool AreEquals(RailwayGauge left, RailwayGauge right) =>
            left.TrackGauge == right.TrackGauge &&
            left.Inches == right.Inches &&
            left.Millimeters == right.Millimeters;
        #endregion

        public override int GetHashCode() => HashCode.Combine(TrackGauge, Inches, Millimeters);
    }
}