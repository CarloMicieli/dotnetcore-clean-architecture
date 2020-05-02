﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using NodaMoney;
using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsUseCase :
        ValidatedUseCase<GetCollectionStatisticsInput, IGetCollectionStatisticsOutputPort>,
        IGetCollectionStatisticsUseCase
    {
        private readonly CollectionsService _collectionService;

        public GetCollectionStatisticsUseCase(IGetCollectionStatisticsOutputPort output, CollectionsService collectionService)
            : base(new GetCollectionStatisticsInputValidator(), output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
        }

        protected override async Task Handle(GetCollectionStatisticsInput input)
        {
            var owner = new Owner(input.Owner);

            ICollection? collection = await _collectionService.GetByOwnerAsync(owner);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(owner);
                return;
            }

            var items = collection.Items
                .Select(it => new
                {
                    Count = it.Details?.RollingStocksCount ?? 1,
                    Category = it.Details?.Category ?? CollectionCategory.Unspecified,
                    Year = Year.FromLocalDate(it.AddedDate),
                    it.Price
                })
                .GroupBy(it => new { it.Category, it.Year })
                .Select(it => (ICollectionStatsItem)new CollectionStatsItem(
                    it.Key.Year,
                    it.Key.Category,
                    it.Sum(y => y.Count),
                    it.Select(y => y.Price).Sum()))
                .ToImmutableList();

            var totalValue = items.Select(it => it.TotalValue).Sum();

            ICollectionStats statistics = new CollectionStats(
                collection.CollectionId,
                collection.Owner,
                collection.ModifiedDate ?? collection.CreatedDate,
                totalValue,
                items);

            OutputPort.Standard(new GetCollectionStatisticsOutput(statistics));
        }
    }

    public static class MoneyExtensions
    {
        public static Money Sum(this IEnumerable<Money> en)
        {
            Money? zero = en.FirstOrDefault();
            if (zero.HasValue)
            {
                return en.Skip(1).Aggregate(zero.Value, (acc, l) => acc + l);
            }

            return Money.Euro(0M);
        }
    }
}