﻿using Xunit;
using FluentAssertions;
using TreniniDotNet.TestHelpers.SeedData.Catalog;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.V1.Catalog
{
    public class ScaleViewTests
    {
        [Fact]
        public void ScaleView_ShouldRenderScales()
        {
            var view = new ScaleView(CatalogSeedData.Scales.ScaleH0(), new LinksView());

            view.Should().NotBeNull();
            view.Name.Should().Be("H0");
            view.Ratio.Should().Be(87.0M);
            view.Gauge.Millimeters.Should().Be(16.5M);
            view.Gauge.Inches.Should().Be(0.65M);

        }
    }
}
