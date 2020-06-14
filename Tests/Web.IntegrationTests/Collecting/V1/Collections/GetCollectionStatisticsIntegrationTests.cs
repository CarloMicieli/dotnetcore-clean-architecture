using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Collecting.V1.Collections.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Collections
{
    public class GetCollectionStatisticsIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/collections/statistics";

        public GetCollectionStatisticsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturn401Unauthorized_WhenUserIsNotAuthenticated()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();

            var response = await client.GetAsync($"api/v1/collections/{id}/statistics");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturn404NotFound_WhenTheCollectionIsNotFound()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();

            var response = await client.GetAsync($"api/v1/collections/{id}/statistics");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturn404NotFound_WhenUserIsNotTheCollectionOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = CollectionSeedData.Collections.GeorgeCollection().Id;

            var response = await client.GetAsync($"api/v1/collections/{id}/statistics");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetCollectionStatistics_ShouldReturnTheCollectionStatistics()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var id = CollectionSeedData.Collections.GeorgeCollection().Id;

            var statistics = await client.GetJsonAsync<CollectionStatisticsResponse>($"api/v1/collections/{id}/statistics");

            statistics.Should().NotBeNull();
            statistics.Id.Should().Be(id.ToGuid());
            statistics.Owner.Should().Be("George");
        }
    }
}