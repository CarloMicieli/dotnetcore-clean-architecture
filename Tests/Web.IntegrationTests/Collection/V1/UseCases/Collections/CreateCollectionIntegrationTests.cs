using FluentAssertions;
using IntegrationTests;
using System;
using System.Net;
using System.Threading.Tasks;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collection.V1.UseCases.Collections
{
    public class CreateCollectionIntegrationTests : AbstractWebApplicationFixture
    {
        private const string CollectionsUri = "/api/v1/collections";

        public CreateCollectionIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task CreateCollection_ShouldFailForNotAuthorizedUsers()
        {
            var client = CreateHttpClient();

            var response = await client.PostJsonAsync(CollectionsUri, new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task CreateCollection_ShouldReturn409Conflict_WhenUserHasAlreadyCollection()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var response = await client.PostJsonAsync(CollectionsUri, new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }

        [Fact]
        public async Task CreateCollection_ShouldCreateNewCollections()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var request = new
            {
                Notes = "My first wonderful collection"
            };

            var response = await client.PostJsonAsync(CollectionsUri, request, Check.IsSuccessful);

            var content = await response.ExtractContent<CollectionCreated>();
            content.Should().NotBeNull();
            content.Id.Should().NotBeEmpty();
            content.Owner.Should().Be("Ciccins");
        }
    }

    internal class CollectionCreated
    {
        public Guid Id { set; get; }
        public string Owner { set; get; }
    }
}