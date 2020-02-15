﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Infrastructure.Persistence;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Brands;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Railways;
using TreniniDotNet.Infrastructure.Persistence.Catalog.Scales;

namespace TreniniDotNet.Web.DependencyInjection
{
    public static class PersistanceExtensions
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration Configuration)
        {
            var connectionString = Configuration.GetConnectionString("ApplicationConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connectionString, b => b.MigrationsAssembly("Web"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCatalogPersistence();

            return services;
        }

        private static IServiceCollection AddCatalogPersistence(this IServiceCollection services)
        {
            services.AddScoped<IBrandsRepository, BrandsRepository>();
            services.AddScoped<IBrandsFactory, BrandsFactory>();

            services.AddScoped<IRailwaysRepository, RailwaysRepository>();
            services.AddScoped<IRailwaysFactory, RailwaysFactory>();

            services.AddScoped<IScalesRepository, ScalesRepository>();
            services.AddScoped<IScalesFactory, ScalesFactory>();

            return services;
        }
    }
}

