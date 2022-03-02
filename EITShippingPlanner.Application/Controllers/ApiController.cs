using EITShippingPlanner.Application.Dto.Api;
using EITShippingPlanner.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IRouteCalculationService _routeCalculationService;

        public ApiController(IRouteCalculationService routeCalculationService)
        {
            _routeCalculationService = routeCalculationService;
        }

        [HttpPost]
        [Route("calculate-segment")]
        public async Task<IActionResult> CalculateSegment([FromBody] CalculationApiRequest request)
        {
            var result = await _routeCalculationService.CalculateSegmentByShipForApi(request);

            if (result.ResponseCode == 200)
            {
                return StatusCode(result.ResponseCode, result);
            }
            else
            {
                return StatusCode(result.ResponseCode);
            }
        }

        [HttpGet]
        [Route("routes")]
        public async Task<IActionResult> AllShipRoutes()
        {
            var result = await _routeCalculationService.GetAllShipRoutes();
            if (result != null)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
