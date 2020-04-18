﻿using NodaMoney;
using NodaTime;
using System;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishlistService
    {
        private readonly IWishlistsRepository _wishlists;
        private readonly IWishlistItemsRepository _wishlistItems;
        public readonly IWishlistsFactory _wishlistsFactory;
        private readonly ICatalogRefsRepository _catalog;

        public WishlistService(
            IWishlistsRepository wishlists,
            IWishlistItemsRepository wishlistItems,
            IWishlistsFactory wishlistsFactory,
            ICatalogRefsRepository catalog)
        {
            _wishlists = wishlists ??
                throw new ArgumentNullException(nameof(wishlists));
            _wishlistItems = wishlistItems ??
                throw new ArgumentNullException(nameof(wishlistItems));
            _wishlistsFactory = wishlistsFactory ??
                throw new ArgumentNullException(nameof(wishlistsFactory));
            _catalog = catalog ??
                throw new ArgumentNullException(nameof(catalog));
        }

        public Task<bool> ExistAsync(WishlistId id) =>
            _wishlists.ExistAsync(id);

        public Task<bool> ExistAsync(Owner owner, Slug wishlistSlug) =>
            _wishlists.ExistAsync(owner, wishlistSlug);

        public Task<WishlistId> CreateWishlist(Owner owner, Slug slug, string listName, Visibility visibility)
        {
            var wishList = _wishlistsFactory.NewWishlist(owner, slug, listName, visibility);
            return _wishlists.AddAsync(wishList);
        }

        public Task<ICatalogRef> GetCatalogRef(Slug catalogItemSlug) =>
            _catalog.GetBySlugAsync(catalogItemSlug);

        public Task<IWishlistItem> GetItemByCatalogRefAsync(WishlistId id, ICatalogRef catalogRef) =>
            _wishlistItems.GetItemByCatalogRefAsync(id, catalogRef);

        public Task<WishlistItemId> AddItemAsync(
            WishlistId id,
            ICatalogRef catalogRef,
            Priority priority,
            LocalDate addedDate,
            Money? price,
            string? notes)
        {
            var newItem = _wishlistsFactory.NewWishlistItem(catalogRef, priority, addedDate, price, notes);
            return _wishlistItems.AddItemAsync(id, newItem);
        }
    }
}
