﻿using System;
using System.Collections.Immutable;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesFactory
    {
        IScale NewScale(ScaleId id,
            string name, Slug slug,
            Ratio ratio,
            ScaleGauge gauge,
            string? description,
            IImmutableSet<ScaleStandard> standards,
            int? weight);

        IScale? NewScale(Guid scaleId,
            string name, string slug,
            decimal ratio,
            decimal gaugeMm, decimal gaugeIn, string trackType,
            string? description,
            int? weight,
            DateTime? lastModified,
            int? version);
    }
}
