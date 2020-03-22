﻿using System;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class CatalogItemWithRelatedData
    {
        public Guid catalog_item_id { set; get; } = default;
        public Guid brand_id { set; get; } = default;
        public string brand_name { set; get; } = null!;
        public string brand_slug { set; get; } = null!;
        public Guid scale_id { set; get; } = default;
        public string scale_name { set; get; } = null!;
        public string scale_slug { set; get; } = null!;
        public decimal scale_ratio { set; get; } = default;
        public string item_number { set; get; } = null!;
        public string slug { set; get; } = null!;
        public string power_method { set; get; } = null!;
        public string? delivery_date { set; get; }
        public string description { set; get; } = null!;
        public string? model_description { set; get; }
        public string? prototype_description { set; get; }
        public Guid rolling_stock_id { set; get; } = default;
        public Guid railway_id { get; set; } = default;
        public string railway_name { get; set; } = null!;
        public string railway_slug { get; set; } = null!;
        public string? railway_country { get; set; }
        public string era { set; get; } = null!;
        public string category { set; get; } = null!;
        public decimal? length { get; set; }
        public string? class_name { get; set; }
        public string? road_number { get; set; }
        public string? type_name { get; set; }
        public string? dcc_interface { get; set; }
        public string? control { get; set; }
        public DateTime? created_at { set; get; }
        public int? version { set; get; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}