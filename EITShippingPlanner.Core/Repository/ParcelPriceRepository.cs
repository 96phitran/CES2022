using EITShippingPlanner.Core.Interface;
using EITShippingPlanner.Core.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EITShippingPlanner.Core.Repository
{
    public class ParcelPriceRepository : IParcelPriceRepository
    {
        protected IEITShippingPlannerDbContext _context;

        public ParcelPriceRepository(IEITShippingPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ParcelPrice>> GetParcelPrices()
        {
            return await _context.ParcelPrices.ToListAsync();
        }

        public async Task<ParcelPrice> GetParcelPriceById(int parcelPriceId)
        {
            return await _context.ParcelPrices.FirstOrDefaultAsync(x => x.Id == parcelPriceId);
        }

        public async Task AddParcelPrices(IList<ParcelPrice> parcelPrices)
        {
            await _context.ParcelPrices.AddRangeAsync(parcelPrices);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParcelPrices(IList<ParcelPrice> parcelPrices)
        {
            _context.ParcelPrices.UpdateRange(parcelPrices);
            await _context.SaveChangesAsync();
        }
    }
}
