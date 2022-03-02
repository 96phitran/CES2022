using EITShippingPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EITShippingPlanner.Core.Interface
{
    public interface IEITShippingPlannerDbContext
    {
        DbSet<CargoCenterLocation> CargoCenterLocations { get; set; }

        DbSet<Route> Routes { get; set; }

        DbSet<ParcelPrice> ParcelPrices { get; set; }

        DbSet<ExtraCharge> ExtraCharges { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
