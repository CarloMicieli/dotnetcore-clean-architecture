﻿using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Wishlists
{
    public sealed class WishlistInfo : IWishlistInfo, IEquatable<WishlistInfo>
    {
        public WishlistInfo(WishlistId wishlistId, Slug slug, string? listName, Visibility visibility)
        {
            WishlistId = wishlistId;
            Slug = slug;
            ListName = listName;
            Visibility = visibility;
        }

        public WishlistId WishlistId { get; }

        public Slug Slug { get; }

        public string? ListName { get; }

        public Visibility Visibility { get; }

        #region [ Equality ]

        public bool Equals(WishlistInfo other) =>
            this.WishlistId == other.WishlistId;

        public override bool Equals(object obj)
        {
            if (obj is WishlistInfo that)
            {
                return this.Equals(that);
            }

            return false;
        }

        #endregion

        public override int GetHashCode() => WishlistId.GetHashCode();
    }
}