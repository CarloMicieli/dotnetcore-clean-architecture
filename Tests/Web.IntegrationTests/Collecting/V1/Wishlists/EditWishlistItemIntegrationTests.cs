using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using TreniniDotNet.Web;
using Xunit;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class EditWishlistItemIntegrationTests : AbstractWebApplicationFixture
    {
        public EditWishlistItemIntegrationTests(CustomWebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = await CreateHttpClientAsync("Ciccins", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();
            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldReturn404NotFound_WhenWishlistItemIsNotFound()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();

            var id = wishlist.Id;
            var itemId = Guid.NewGuid();

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", new { }, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task EditWishlistItem_ShouldModifyWishlistItem()
        {
            var client = await CreateHttpClientAsync("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.GeorgeFirstList();
            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var request = new
            {
                Price = 250M,
                Priority = "High",
                Notes = "My notes"
            };

            var response = await client.PutJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", request, Check.Nothing);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
