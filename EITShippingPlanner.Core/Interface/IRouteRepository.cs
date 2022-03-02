using EITShippingPlanner.Core.Enum;
using EITShippingPlanner.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EITShippingPlanner.Core.Interface
{
    public interface IRouteRepository
    {
        Task<IList<Route>> GetRoutes();

        Task<IList<Route>> GetAllRoutesByTransportationType(TransportationType transportationType);

        Task<Route> GetRoutesById(int routeId);

        Task AddRoutes(IList<Route> routes);
    }
}
