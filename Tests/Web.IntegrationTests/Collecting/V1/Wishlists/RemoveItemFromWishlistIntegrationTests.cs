using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.IntegrationTests.Helpers.Extensions;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using TreniniDotNet.Web;
using Xunit;
using Xunit.Abstractions;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Wishlists
{
    public class RemoveItemFromWishlistIntegrationTests : AbstractWebApplicationFixture
    {
        public RemoveItemFromWishlistIntegrationTests(CustomWebApplicationFactory<Startup> factory, ITestOutputHelper output)
            : base(factory, output)
        {
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldReturn401Unauthorized_WhenUserIsNotAuthorized()
        {
            var client = CreateHttpClient();

            var id = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id}/items/{itemId}", Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldReturn404NotFound_WhenUserIsNotTheWishlistOwner()
        {
            var client = CreateHttpClient("Ciccins", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id.ToGuid()}/items/{itemId.ToGuid()}", Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldReturn404NotFound_WhenWishlistItemIsNotFound()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var id = wishlist.Id;
            var itemId = Guid.NewGuid();

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id.ToGuid()}/items/{itemId}", Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task RemoveItemFromWishlist_ShouldRemoveItemFromWishlist()
        {
            var client = CreateHttpClient("George", "Pa$$word88");

            var wishlist = CollectingSeedData.Wishlists.NewGeorgeFirstList();

            var id = wishlist.Id;
            var itemId = wishlist.Items.First().Id;

            var response = await client.DeleteJsonAsync($"/api/v1/wishlists/{id.ToGuid()}/items/{itemId.ToGuid()}", Check.Nothing);

            await response.LogAsyncTo(Output);

            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
