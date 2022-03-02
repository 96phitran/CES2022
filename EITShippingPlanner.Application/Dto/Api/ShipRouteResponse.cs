using System.Text.Json.Serialization;

namespace EITShippingPlanner.Application.Dto.Api
{
    public class ShipRouteResponse
    {
        public ShipRouteResponse(string cityA, string cityB, int segment)
        {
            CityA = cityA;
            CityB = cityB;
            Segment = segment;
        }

        [JsonPropertyName("citya")]
        public string CityA { get; set; }

        [JsonPropertyName("cityb")]
        public string CityB { get; set; }

        [JsonPropertyName("segment")]
        public int Segment { get; set; }
    }
}
