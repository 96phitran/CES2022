using EITShippingPlanner.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EITShippingPlanner.Core.Enum.ParcelType;

namespace EITShippingPlanner.Core.Interface
{
    public interface IExtraChargeRepository
    {
        public Task<IList<ExtraCharge>> GetExtraCharges();

        public Task<ExtraCharge> GetExtraChargeById(int extraChargeId);

        public Task<ExtraCharge> GetExtraChargeByParcelType(ParcelTypeEnum parcelType);

        public Task AddExtraCharge(ExtraCharge extraCharge);
    }
}
