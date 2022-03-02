using System.Text.Json.Serialization;

namespace EITShippingPlanner.Application.Dto.Api
{
    public class CalculationApiResponse
    {
        public CalculationApiResponse() { }

        public CalculationApiResponse(int responseCode)
        {
            ResponseCode = responseCode;
        }

        public CalculationApiResponse(int time, float price, int responseCode)
        {
            Time = time;
            Price = price;
            ResponseCode = responseCode;
        }

        [JsonPropertyName("time")]
        public int Time { get; set; }

        [JsonPropertyName("price")]
        public float Price { get; set; }

        [JsonIgnore]
        public int ResponseCode { get; set; }
    }
}
