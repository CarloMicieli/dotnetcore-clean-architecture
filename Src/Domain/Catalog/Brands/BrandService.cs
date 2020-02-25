﻿using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandService
    {
        private readonly IBrandsRepository _brandRepository;

        public BrandService(IBrandsRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public Task<BrandId> CreateBrand(string name, Slug slug, string? companyName, string? websiteUrl, string? emailAddress, BrandKind kind)
        {
            return _brandRepository.Add(
                BrandId.NewId(),
                name,
                slug,
                companyName,
                new Uri(websiteUrl),
                new MailAddress(emailAddress),
                kind);
        }

        public Task<PaginatedResult<IBrand>> FindAllBrands(Page? page)
        {
            return _brandRepository.GetBrands(page ?? Page.Default);
        }

        public Task<IBrand> GetBy(Slug slug)
        {
            return _brandRepository.GetBy(slug);
        }

        public Task<bool> BrandAlreadyExists(Slug slug)
        {
            return _brandRepository.Exists(slug);
        }
    }
}
