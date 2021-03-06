﻿namespace TreniniDotNet.Web.Catalog.V1.Brands.Common.Requests
{
    public sealed class AddressRequest
    {
        public string? Line1 { set; get; }
        public string? Line2 { set; get; }
        public string? City { set; get; }
        public string? Region { set; get; }
        public string? PostalCode { set; get; }
        public string? Country { set; get; }
    }
}
