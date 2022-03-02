using EITShippingPlanner.Application.Interface;
using EITShippingPlanner.Application.Service;
using EITShippingPlanner.Core.Infrastructure.Database;
using EITShippingPlanner.Core.Interface;
using EITShippingPlanner.Core.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("db");

            services.AddDbContext<IEITShippingPlannerDbContext, EITShippingPlannerDbContext>(
                opt => opt.UseSqlServer(connectionString, builder =>
                {
                    builder.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
                    builder.EnableRetryOnFailure(12, TimeSpan.FromSeconds(30), null);
                }
                ));

            services.AddDbContext<EITShippingPlannerDbContext>();

            services.AddControllersWithViews();

            services.AddOptions();
            services.AddMemoryCache();

            // Dependency Injection
            // Services
            services.AddScoped<IRouteCalculationService, RouteCalculationService>();
            services.AddScoped<IPriceUpdatingService, PriceUpdatingService>();

            // Repositories
            services.AddScoped<ICargoCenterLocationRepository, CargoCenterLocationRepository>();
            services.AddScoped<IParcelPriceRepository, ParcelPriceRepository>();
            services.AddScoped<IExtraChargeRepository, ExtraChargeRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            UpdateDatabase(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=RouteCalculator}/{action=Index}/{id?}");
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<EITShippingPlannerDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
