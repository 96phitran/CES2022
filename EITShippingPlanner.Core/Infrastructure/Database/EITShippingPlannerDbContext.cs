using EITShippingPlanner.Core.Interface;
using EITShippingPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace EITShippingPlanner.Core.Infrastructure.Database
{
    public class EITShippingPlannerDbContext : DbContext, IEITShippingPlannerDbContext
    {
        public EITShippingPlannerDbContext(DbContextOptions<EITShippingPlannerDbContext> options) : base(options) { }

        public EITShippingPlannerDbContext() { }

        public DbSet<CargoCenterLocation> CargoCenterLocations { get; set; }

        public DbSet<Route> Routes { get; set; }

        public DbSet<ParcelPrice> ParcelPrices { get; set; }

        public DbSet<ExtraCharge> ExtraCharges { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<CargoCenterLocation>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.HasIndex(e => e.Code);
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.HasIndex(e => e.TransportationType);

                entity.HasIndex(e => e.FirstLocationId);

                entity.HasIndex(e => e.SecondLocationId);

                entity.HasOne(e => e.FirstLocation)
                    .WithMany(q => q.RouteFrom)
                    .HasForeignKey(e => e.FirstLocationId);

                entity.HasOne(e => e.SecondLocation)
                    .WithMany(q => q.RouteTo)
                    .HasForeignKey(e => e.SecondLocationId);
            });

            modelBuilder.Entity<ParcelPrice>(entity =>
            {
                entity.HasIndex(e => e.Id);
            });

            modelBuilder.Entity<ExtraCharge>(entity =>
            {
                entity.HasIndex(e => e.Id);

                entity.HasIndex(e => e.ParcelType);
            });
        }
    }
}
