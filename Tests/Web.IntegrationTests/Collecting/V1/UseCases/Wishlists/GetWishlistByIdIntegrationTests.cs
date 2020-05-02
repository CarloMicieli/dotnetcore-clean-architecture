using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Collecting.V1.Responses;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collection;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.UseCases.Wishlists
{
    public class GetWishlistByIdIntegrationTests : AbstractWebApplicationFixture
    {
        protected string EndpointUrl => "api/v1/wishlists";

        public GetWishlistByIdIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var response = await client.GetAsync($"/api/v1/wishlists/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturn404_WhenTheWishlistWasNotFound()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = Guid.NewGuid();
            var response = await client.GetAsync($"/api/v1/wishlists/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturn404_WhenTheUserIsNotTheOwnerOfPrivateWishlist()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var id = CollectionSeedData.Wishlists.George_First_List().WishlistId;
            var response = await client.GetAsync($"/api/v1/wishlists/{id}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetWishlistById_ShouldReturnWishlist()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var id = CollectionSeedData.Wishlists.George_First_List().WishlistId;
            var wishlist = await client.GetJsonAsync<WishlistResponse>($"/api/v1/wishlists/{id}");

            wishlist.Should().NotBeNull();
            wishlist.Owner.Should().Be("George");
        }
    }
}