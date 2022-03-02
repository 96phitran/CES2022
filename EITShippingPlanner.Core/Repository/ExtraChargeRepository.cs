using EITShippingPlanner.Core.Infrastructure.Database;
using EITShippingPlanner.Core.Interface;
using EITShippingPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using static EITShippingPlanner.Core.Enum.ParcelType;

namespace EITShippingPlanner.Core.Repository
{
    public class ExtraChargeRepository : IExtraChargeRepository
    {
        private readonly IEITShippingPlannerDbContext _context;

        public ExtraChargeRepository(IEITShippingPlannerDbContext context)
        {
            this._context = context;
        }

        public async Task<IList<ExtraCharge>> GetExtraCharges()
        {
            return await _context.ExtraCharges.ToListAsync();
        }

        public async Task<ExtraCharge> GetExtraChargeById(int extraChargeId)
        {
            return await _context.ExtraCharges.FirstOrDefaultAsync(x => x.Id == extraChargeId);
        }

        public async Task<ExtraCharge> GetExtraChargeByParcelType(ParcelTypeEnum parcelType)
        {
            return await _context.ExtraCharges.FirstOrDefaultAsync(x => x.ParcelType == parcelType);
        }

        public async Task AddExtraCharge(ExtraCharge extraCharge)
        {
            _context.ExtraCharges.Add(extraCharge);
            await _context.SaveChangesAsync();
        }
    }
}
