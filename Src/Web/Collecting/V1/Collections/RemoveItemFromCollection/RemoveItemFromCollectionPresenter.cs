﻿using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionPresenter : DefaultHttpResultPresenter<RemoveItemFromCollectionOutput>, IRemoveItemFromCollectionOutputPort
    {
        public void CollectionNotFound(CollectionId collectionId)
        {
            ViewModel = new NotFoundResult();
        }

        public void CollectionItemNotFound(CollectionId collectionId, CollectionItemId itemId)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(RemoveItemFromCollectionOutput output)
        {
            ViewModel = new NoContentResult();
        }
    }
}
