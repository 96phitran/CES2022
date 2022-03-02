using EITShippingPlanner.Core.Enum;
using EITShippingPlanner.Core.Infrastructure.Database;
using EITShippingPlanner.Core.Interface;
using EITShippingPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EITShippingPlanner.Core.Repository
{
    public class RouteRepository : IRouteRepository
    {
        private readonly IEITShippingPlannerDbContext _context;

        public RouteRepository(IEITShippingPlannerDbContext context)
        {
            this._context = context;
        }

        public async Task<IList<Route>> GetRoutes()
        {
            return await _context.Routes
                .Include(x => x.FirstLocation)
                .Include(x => x.SecondLocation).ToListAsync();
        }

        public async Task<IList<Route>> GetAllRoutesByTransportationType(TransportationType transportationType)
        {
            return await _context.Routes
                .Where(x => x.TransportationType == transportationType)
                .Include(x => x.FirstLocation)
                .Include(x => x.SecondLocation)
                .ToListAsync();
        }

        public async Task<Route> GetRoutesById(int routeId)
        {
            return await _context.Routes.FirstOrDefaultAsync(x => x.Id == routeId);
        }

        public async Task AddRoutes(IList<Route> routes)
        {
            await _context.Routes.AddRangeAsync(routes);
            await _context.SaveChangesAsync();
        }
    }
}
