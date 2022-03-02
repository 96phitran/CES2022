using EITShippingPlanner.Core.Infrastructure.Database;
using EITShippingPlanner.Core.Interface;
using EITShippingPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EITShippingPlanner.Core.Repository
{
    public class CargoCenterLocationRepository : ICargoCenterLocationRepository
    {
        private readonly EITShippingPlannerDbContext _context;

        public CargoCenterLocationRepository(EITShippingPlannerDbContext context)
        {
            this._context = context;
        }
        public async Task<IList<CargoCenterLocation>> GetCargoLocations()
        {
            return await _context.CargoCenterLocations.ToListAsync();
        }
        public async Task<CargoCenterLocation> GetCargoLocationById(int cargoId)
        {
            return await _context.CargoCenterLocations.FirstOrDefaultAsync(x => x.Id == cargoId);
        }

        public async Task AddCargoLocation(CargoCenterLocation cargoCenterLocation)
        {
            _context.CargoCenterLocations.Add(cargoCenterLocation);
            await _context.SaveChangesAsync();
        }
    }
}
