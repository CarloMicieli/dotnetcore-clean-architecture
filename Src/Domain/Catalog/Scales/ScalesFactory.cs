using NodaTime;
using System;
using System.Collections.Immutable;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScalesFactory : IScalesFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public ScalesFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));
            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IScale NewScale(ScaleId id,
            string name, Slug slug,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            IImmutableSet<ScaleStandard> standards,
            int? weight)
        {
            return new Scale(
                id,
                name, slug,
                ratio,
                gauge,
                description,
                 standards,
                weight,
                _clock.GetCurrentInstant(),
                1);
        }

        public IScale? NewScale(Guid id,
            string name, string slug,
            decimal ratio,
            decimal gaugeMm, decimal gaugeIn, string trackType,
            string? description,
            int? weight,
            DateTime? lastModified,
            int? version)
        {
            var scaleGauge = ScaleGauge.Of(gaugeMm, gaugeIn, trackType);

            return new Scale(
                new ScaleId(id),
                name, Slug.Of(slug),
                Ratio.Of(ratio),
                scaleGauge,
                description,
                ImmutableHashSet<ScaleStandard>.Empty,
                weight,
                lastModified.ToUtcOrGetCurrent(_clock),
                version ?? 1);
        }
    }
}