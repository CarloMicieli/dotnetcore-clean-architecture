﻿namespace TreniniDotNet.IntegrationTests.Collecting.V1.Common.Responses
{
    public class CatalogItemDetailsResponse
    {
        public BrandRefResponse Brand { set; get; }
        public string ItemNumber { set; get; }
        public string Category { set; get; }
        public ScaleRefResponse Scale { set; get; }
        public int Count { set; get; }
        public string Description { set; get; }
    }
}
