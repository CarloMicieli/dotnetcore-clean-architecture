﻿using FluentAssertions;
using IntegrationTests;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Catalog.V1.Responses;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Catalog.V1.UseCases
{
    public sealed class GetBrandsListIntegrationTests : AbstractWebApplicationFixture
    {
        public GetBrandsListIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturn200OK_AndTheBrands()
        {
            var content = await GetJsonAsync<BrandsListResponse>("/api/v1/brands");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be("http://localhost/api/v1/Brands?start=0&limit=50");

            content.Results.Should().NotBeEmpty();

            var firstResult = content.Results.First();
            firstResult._Links.Should().NotBeNull();
            firstResult._Links._Self.Should().Be("http://localhost/api/v1/Brands/acme");
            firstResult._Links.Slug.Should().Be("acme");
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturn200OK_AndTheFirstPageOfBrands()
        {
            var limit = 2;
            var content = await GetJsonAsync<BrandsListResponse>($"/api/v1/brands?start=0&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Brands?start=0&limit={limit}");
            content._links.Prev.Should().BeNull();
            content._links.Next.Should().Be($"http://localhost/api/v1/Brands?start={limit}&limit={limit}");

            content.Limit.Should().Be(limit);

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }

        [Fact]
        public async Task GetBrandsList_ShouldReturn200OK_AndOnePageOfBrands()
        {
            var limit = 2;
            var content = await GetJsonAsync<BrandsListResponse>($"/api/v1/brands?start=2&limit={limit}");

            content._links.Should().NotBeNull();
            content._links._Self.Should().Be($"http://localhost/api/v1/Brands?start=2&limit={limit}");
            content._links.Prev.Should().Be($"http://localhost/api/v1/Brands?start=0&limit={limit}");
            content._links.Next.Should().Be($"http://localhost/api/v1/Brands?start=4&limit={limit}");

            content.Limit.Should().Be(limit);

            content.Results.Should().NotBeEmpty();
            content.Results.Should().HaveCount(limit);
        }
    }
}
