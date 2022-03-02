using EITShippingPlanner.Application.Interface;
using EITShippingPlanner.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application.Controllers
{
    public class PriceUpdateController : Controller
    {
        private readonly ILogger<PriceUpdateController> _logger;
        private readonly IPriceUpdatingService _priceUpdatingService;

        public PriceUpdateController(ILogger<PriceUpdateController> logger, IPriceUpdatingService priceUpdatingService)
        {
            _logger = logger;
            _priceUpdatingService = priceUpdatingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var priceModel = await _priceUpdatingService.GetParcelPrices();
            return View(priceModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index([FromForm] PriceUpdateModel model)
        {
            await _priceUpdatingService.UpdatePrice(model);

            TempData["updated"] = true;
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
