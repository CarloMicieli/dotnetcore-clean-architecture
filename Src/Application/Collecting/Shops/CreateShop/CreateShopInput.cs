﻿using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.CreateShop
{
    public sealed class CreateShopInput : IUseCaseInput
    {
        public CreateShopInput(string name, string? websiteUrl, string? emailAddress, ShopAddressInput? address, string? phoneNumber)
        {
            Name = name;
            WebsiteUrl = websiteUrl;
            Address = address;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public string Name { get; }

        public string? WebsiteUrl { get; }

        public ShopAddressInput? Address { get; }

        public string? EmailAddress { get; }

        public string? PhoneNumber { get; }
    }
}
