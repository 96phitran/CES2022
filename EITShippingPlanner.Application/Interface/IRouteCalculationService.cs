using EITShippingPlanner.Application.Dto.Api;
using EITShippingPlanner.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application.Interface
{
    public interface IRouteCalculationService
    {
        Task<CalculationApiResponse> CalculateSegmentByShipForApi(CalculationApiRequest request);
        Task<IList<ShipRouteResponse>> GetAllShipRoutes();
        Task<RouteCalculationPageModel> InitializeRoutePageModel();
        Task<RouteCalculationPageModel> FindOptimalRoute(RouteCalculationPageModel request);
    }
}
