using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static EITShippingPlanner.Core.Enum.ParcelType;

namespace EITShippingPlanner.Application.Dto.Api
{
    public class CalculationApiRequest
    {
        [JsonPropertyName("length")]
        public float Length { get; set; }

        [Required]
        [JsonPropertyName("weight")]
        public float Weight { get; set; }

        [Required]
        [JsonPropertyName("segment")]
        public int Segment { get; set; }

        [Required]
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [Required]
        [JsonPropertyName("packageType")]
        public ParcelTypeEnum ParcelType { get; set; }
    }
}
