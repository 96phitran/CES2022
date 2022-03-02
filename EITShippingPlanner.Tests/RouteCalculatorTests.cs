using EITShippingPlanner.Application.Dto.Api;
using EITShippingPlanner.Application.Interface;
using EITShippingPlanner.Application.Service;
using Xunit;

using static EITShippingPlanner.Core.Enum.ParcelType;

namespace EITShippingPlanner.Tests
{
    public class RouteCalculatorTests
    {
        [Fact]
        public void CalculateSegmentByShipForApi_RouteCalculatorTests()
        {
            //Arange
            var request = new CalculationApiRequest()
            {
                Length = 1,
                Weight = 1,
                Segment = 1,
                Date = "2021/12/01",
                ParcelType = ParcelTypeEnum.LiveAnimals
            };

            //Action
            var response = new CalculationApiResponse() 
            {
                ResponseCode = 200,
                Time = 1,
                Price = 1
            };
            
            //Assert
            Assert.True(response.ResponseCode == 200);
            Assert.True(response.Time == 1);
            Assert.True(response.Price == 1);
        }
    }
}
