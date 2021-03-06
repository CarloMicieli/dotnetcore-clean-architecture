using FluentAssertions;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.TestHelpers.SeedData.Collecting;
using Xunit;

namespace TreniniDotNet.Web.Collecting.V1.Shops.Common.ViewModels
{
    public class ShopInfoViewTests
    {
        [Fact]
        public void ShopInfoView_ShouldRenderShops()
        {
            var shop = new ShopRef(CollectingSeedData.Shops.NewModellbahnshopLippe());

            var view = new ShopInfoView(shop);

            view.Name.Should().Be(shop.ToString()); //TODO
            view.Slug.Should().Be(shop.Slug);
            view.ShopId.Should().Be(shop.Id);
        }
    }
}