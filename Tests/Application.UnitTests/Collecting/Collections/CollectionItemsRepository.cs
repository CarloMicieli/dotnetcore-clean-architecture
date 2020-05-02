﻿using System.Linq;
using System.Threading.Tasks;
using NodaTime;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.ValueObjects;
using TreniniDotNet.TestHelpers.InMemory.Repository;

namespace TreniniDotNet.Application.InMemory.Collecting.Collections
{
    public class CollectionItemsRepository : ICollectionItemsRepository
    {
        private readonly InMemoryContext _context;

        public CollectionItemsRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<CollectionItemId> AddItemAsync(CollectionId id, ICollectionItem newItem) =>
            Task.FromResult(newItem.ItemId);

        public Task EditItemAsync(CollectionId id, ICollectionItem item) =>
            Task.CompletedTask;

        public Task<ICollectionItem> GetItemByIdAsync(CollectionId collectionId, CollectionItemId itemId)
        {
            var result = _context.Collections
                .Where(it => it.CollectionId == collectionId)
                .SelectMany(it => it.Items)
                .FirstOrDefault(it => it.ItemId == itemId);
            return Task.FromResult(result);
        }

        public Task<bool> ItemExistsAsync(CollectionId id, CollectionItemId itemId)
        {
            var result = _context.Collections
                .Where(it => it.CollectionId == id)
                .SelectMany(it => it.Items)
                .Any(it => it.ItemId == itemId);
            return Task.FromResult(result);
        }

        public Task RemoveItemAsync(CollectionId collectionId, CollectionItemId itemId, LocalDate? removed) =>
            Task.CompletedTask;
    }
}