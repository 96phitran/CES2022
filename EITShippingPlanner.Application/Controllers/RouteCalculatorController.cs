using EITShippingPlanner.Application.Interface;
using EITShippingPlanner.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application.Controllers
{
    public class RouteCalculatorController : Controller
    {
        private readonly ILogger<RouteCalculatorController> _logger;
        private readonly IRouteCalculationService _routeCalculationService;

        public RouteCalculatorController(ILogger<RouteCalculatorController> logger,
                                         IRouteCalculationService routeCalculationService)
        {
            _logger = logger;
            _routeCalculationService = routeCalculationService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _routeCalculationService.InitializeRoutePageModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] RouteCalculationPageModel request)
        {
            var model = await _routeCalculationService.FindOptimalRoute(request);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
