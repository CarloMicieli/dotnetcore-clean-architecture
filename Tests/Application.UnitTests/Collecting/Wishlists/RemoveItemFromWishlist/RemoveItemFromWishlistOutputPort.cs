﻿using TreniniDotNet.Domain.Collecting.Wishlists;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.Collecting.Wishlists.RemoveItemFromWishlist
{
    public sealed class RemoveItemFromWishlistOutputPort : OutputPortTestHelper<RemoveItemFromWishlistOutput>, IRemoveItemFromWishlistOutputPort
    {
        private MethodInvocation<WishlistId, WishlistItemId> WishlistItemNotFoundMethod { set; get; }

        public RemoveItemFromWishlistOutputPort()
        {
            WishlistItemNotFoundMethod = MethodInvocation<WishlistId, WishlistItemId>.NotInvoked(nameof(WishlistItemNotFound));
        }

        public void WishlistItemNotFound(WishlistId id, WishlistItemId itemId)
        {
            WishlistItemNotFoundMethod = WishlistItemNotFoundMethod.Invoked(id, itemId);
        }

        public void AssertWishlistItemNotFound(WishlistId expectedId, WishlistItemId expectedItemId) =>
            WishlistItemNotFoundMethod.ShouldBeInvokedWithTheArguments(expectedId, expectedItemId);
    }
}
