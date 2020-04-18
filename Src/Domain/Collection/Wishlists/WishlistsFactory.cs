﻿using NodaMoney;
using NodaTime;
using System;
using System.Collections.Immutable;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishlistsFactory : IWishlistsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public WishlistsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));

            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IWishList NewWishlist(Owner owner, Slug slug, string? listName, Visibility visibility) =>
            NewWishlist(
                new WishlistId(_guidSource.NewGuid()),
                owner,
                slug,
                listName,
                visibility,
                ImmutableList<IWishlistItem>.Empty,
                _clock.GetCurrentInstant(),
                null,
                1);

        public IWishList NewWishlist(
            WishlistId wishlistId,
            Owner owner,
            Slug slug, string? listName,
            Visibility visibility,
            IImmutableList<IWishlistItem> items,
            Instant createdDate,
            Instant? modifiedDate,
            int version)
        {
            return new WishList(
                wishlistId,
                owner,
                slug,
                listName,
                visibility,
                items,
                createdDate,
                modifiedDate,
                version);
        }


        public IWishlistItem NewWishlistItem(
            ICatalogRef catalogItem,
            Priority priority,
            LocalDate addedDate,
            Money? price,
            string? notes) => NewWishlistItem(
                new WishlistItemId(_guidSource.NewGuid()),
                catalogItem,
                null,
                priority,
                addedDate,
                price,
                notes);

        public IWishlistItem NewWishlistItem(
            WishlistItemId itemId,
            ICatalogRef catalogItem,
            ICatalogItemDetails? details,
            Priority priority,
            LocalDate addedDate,
            Money? price,
            string? notes) => new WishlistItem(
                itemId,
                catalogItem,
                details,
                priority,
                addedDate,
                price,
                notes);
    }
}
