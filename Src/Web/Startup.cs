using System;
using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NodaTime;
using Serilog;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Infrastructure.HealthChecks;
using TreniniDotNet.Infrastructure.Identity.DependencyInjection;
using TreniniDotNet.Infrastructure.Persistence.DependencyInjection;
using TreniniDotNet.Web.Catalog.V1;
using TreniniDotNet.Web.Collecting.V1;
using TreniniDotNet.Web.Infrastructure.DependencyInjection;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;
using TreniniDotNet.Web.Uploads;

namespace TreniniDotNet.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            services.AddControllers(o =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    o.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });
            services.AddHttpContextAccessor();

            services.AddRepositories(Configuration);
            services.AddUseCases();
            services.AddCatalogPresenters();
            services.AddCollectingPresenter();

            services.AddOpenApi();
            services.AddVersioning();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddSingleton<ILinksGenerator, AspNetLinksGenerator>();

            services.AddSingleton<IGuidSource, GuidSource>();
            services.AddSingleton<IClock>(SystemClock.Instance);

            var uploadsSection = Configuration.GetSection("Uploads");
            services.Configure<UploadSettings>(uploadsSection);

            services.AddIdentityManagement(Configuration);

            services.AddHealthChecks()
                .AddDatabaseHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopmentOrTesting())
            {
                app.UseExceptionHandler("/error-local-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.TagsSorter = "alpha";
                settings.OperationsSorter = "alpha";
            });

            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }

    public static class HostEnvironmentEnvExtensions
    {
        public static bool IsDevelopmentOrTesting(this IHostEnvironment hostEnvironment) =>
            hostEnvironment.IsDevelopment() || "Testing" == hostEnvironment.EnvironmentName;
    }
}
