﻿using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Application.InMemory.Repositories.Catalog
{
    public class BrandRepository : IBrandsRepository
    {
        private readonly InMemoryContext _context;

        public BrandRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Task<BrandId> AddAsync(IBrand brand)
        {
            _context.Brands.Add(brand);
            return Task.FromResult(brand.BrandId);
        }

        public Task<bool> ExistsAsync(Slug slug)
        {
            bool exists = _context.Brands.Any(b => b.Slug == slug);
            return Task.FromResult(exists);
        }

        public Task<PaginatedResult<IBrand>> GetBrandsAsync(Page page)
        {
            var results = _context.Brands
                .OrderBy(r => r.Name)
                .Skip(page.Start)
                .Take(page.Limit + 1)
                .ToList();

            return Task.FromResult(new PaginatedResult<IBrand>(page, results));
        }

        public Task<IBrand> GetByNameAsync(string name)
        {
            IBrand brand = _context.Brands
                .FirstOrDefault(e => e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return Task.FromResult(brand);
        }

        public Task<IBrand> GetBySlugAsync(Slug slug)
        {
            IBrand brand = _context.Brands.FirstOrDefault(e => e.Slug == slug);
            return Task.FromResult(brand);
        }
    }
}
