using EITShippingPlanner.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EITShippingPlanner.Core.Interface
{
    public interface ICargoCenterLocationRepository
    {
        public Task<IList<CargoCenterLocation>> GetCargoLocations();

        public Task<CargoCenterLocation> GetCargoLocationById(int cargoId);

        public Task AddCargoLocation(CargoCenterLocation cargoCenterLocation);
    }
}
