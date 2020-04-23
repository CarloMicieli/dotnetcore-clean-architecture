using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Domain.Collection.Collections;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class CollectionView
    {
        private readonly ICollection _inner;

        public CollectionView(ICollection collection)
        {
            _inner = collection;
            Items = collection.Items.Select(it => new CollectionItemView(it)).ToList();
        }

        public Guid Id => _inner.CollectionId.ToGuid();
        public string Owner => _inner.Owner.Value;
        public IEnumerable<CollectionItemView> Items { get; }
    }
}