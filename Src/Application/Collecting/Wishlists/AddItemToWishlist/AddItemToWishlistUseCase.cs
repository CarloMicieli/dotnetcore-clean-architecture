﻿using System;
using System.Threading.Tasks;
using NodaMoney;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.Domain.Collecting.Wishlists;

namespace TreniniDotNet.Application.Collecting.Wishlists.AddItemToWishlist
{
    public sealed class AddItemToWishlistUseCase : ValidatedUseCase<AddItemToWishlistInput, IAddItemToWishlistOutputPort>, IAddItemToWishlistUseCase
    {
        private readonly WishlistService _wishlistService;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemToWishlistUseCase(IAddItemToWishlistOutputPort output, WishlistService wishlistService, IUnitOfWork unitOfWork)
            : base(new AddItemToWishlistInputValidator(), output)
        {
            _wishlistService = wishlistService ??
                throw new ArgumentNullException(nameof(wishlistService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(AddItemToWishlistInput input)
        {
            var id = new WishlistId(input.Id);
            var owner = new Owner(input.Owner);

            var wishlistExists = await _wishlistService.ExistAsync(owner, id);
            if (wishlistExists == false)
            {
                OutputPort.WishlistNotFound(id);
                return;
            }

            var catalogItemSlug = Slug.Of(input.CatalogItem);
            var catalogRef = await _wishlistService.GetCatalogRef(catalogItemSlug);
            if (catalogRef is null)
            {
                OutputPort.CatalogItemNotFound(catalogItemSlug);
                return;
            }

            var itemIdForCatalogItem = await _wishlistService.GetItemIdByCatalogRefAsync(id, catalogRef);
            if (itemIdForCatalogItem.HasValue)
            {
                OutputPort.CatalogItemAlreadyPresent(id, itemIdForCatalogItem.Value, catalogRef);
                return;
            }

            Money? price = input.Price.HasValue ?
                Money.Euro(input.Price.Value) : (Money?)null; //TODO: fix currency management

            Priority priority = EnumHelpers.OptionalValueFor<Priority>(input.Priority) ??
                Priority.Normal;

            var itemId = await _wishlistService.AddItemAsync(id,
                catalogRef,
                priority,
                input.AddedDate.ToLocalDate(),
                price,
                input.Notes);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new AddItemToWishlistOutput(id, itemId));
        }
    }
}