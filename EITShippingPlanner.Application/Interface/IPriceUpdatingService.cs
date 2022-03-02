using EITShippingPlanner.Application.Models;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application.Interface
{
    public interface IPriceUpdatingService
    {
        Task UpdatePrice(PriceUpdateModel model);
        Task<PriceUpdateModel> GetParcelPrices();
    }
}
