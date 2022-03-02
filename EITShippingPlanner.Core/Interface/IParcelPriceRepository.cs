using EITShippingPlanner.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EITShippingPlanner.Core.Interface
{
    public interface IParcelPriceRepository
    {
        Task<IList<ParcelPrice>> GetParcelPrices();

        Task<ParcelPrice> GetParcelPriceById(int parcelPriceId);

        Task AddParcelPrices(IList<ParcelPrice> parcelPrices);

        Task UpdateParcelPrices(IList<ParcelPrice> parcelPrices);
    }
}
