using EITShippingPlanner.Application.Interface;
using EITShippingPlanner.Application.Models;
using EITShippingPlanner.Core.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application.Service
{
    public class PriceUpdatingService : IPriceUpdatingService
    {
        private readonly IParcelPriceRepository _parcelPriceRepository;

        public PriceUpdatingService(IParcelPriceRepository parcelPriceRepository)
        {
            _parcelPriceRepository = parcelPriceRepository;
        }

        public async Task UpdatePrice(PriceUpdateModel model)
        {
            var parcelPrices = await _parcelPriceRepository.GetParcelPrices();
            for (int i = 0; i < parcelPrices.Count; i++)
            {
                var currentBasePrice = parcelPrices.FirstOrDefault(x => x.Id == i+1);
                switch (currentBasePrice.Id)
                {
                    case 1:
                        currentBasePrice.Price = model.Under10InNovToApr;
                        break;
                    case 2:
                        currentBasePrice.Price = model.From10To50InNovToApr;
                        break;
                    case 3:
                        currentBasePrice.Price = model.Above50InNovToApr;
                        break;
                    case 4:
                        currentBasePrice.Price = model.Under10InMayToOct;
                        break;
                    case 5:
                        currentBasePrice.Price = model.From10To50InMayToOct;
                        break;
                    case 6:
                        currentBasePrice.Price = model.Above50InMayToOct;
                        break;
                    default:
                        throw new Exception("Price range not existed");
                    }
            }
            await _parcelPriceRepository.UpdateParcelPrices(parcelPrices);
        }

        public async Task<PriceUpdateModel> GetParcelPrices()
        {
            var parcelPrices = await _parcelPriceRepository.GetParcelPrices();
            var priceUpdateModel = new PriceUpdateModel();
            for (int i = 0; i < parcelPrices.Count; i++)
            {
                var currentBasePrice = parcelPrices.FirstOrDefault(x => x.Id == i+1);
                switch (currentBasePrice.Id)
                {
                    case 1:
                        priceUpdateModel.Under10InNovToApr = currentBasePrice.Price;
                        break;
                    case 2:
                        priceUpdateModel.From10To50InNovToApr = currentBasePrice.Price;
                        break;
                    case 3:
                        priceUpdateModel.Above50InNovToApr = currentBasePrice.Price;
                        break;
                    case 4:
                        priceUpdateModel.Under10InMayToOct = currentBasePrice.Price;
                        break;
                    case 5:
                        priceUpdateModel.From10To50InMayToOct = currentBasePrice.Price;
                        break;
                    case 6:
                        priceUpdateModel.Above50InMayToOct = currentBasePrice.Price;
                        break;
                    default:
                        throw new Exception("Price range not existed");
                }
            }
            return priceUpdateModel;
        }
    }
}
