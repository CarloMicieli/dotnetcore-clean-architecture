﻿using System.Collections.Generic;
using TreniniDotNet.IntegrationTests.Catalog.V1.Responses;

namespace TreniniDotNet.IntegrationTests.Collecting.V1.Responses
{
    internal class ShopsListResponse
    {
        public PaginationLinks _links { get; set; }
        public int? Limit { get; set; }
        public List<ShopInfoResponse> Results { get; set; }
    }
}