using static EITShippingPlanner.Core.Enum.ParcelType;

namespace EITShippingPlanner.Core.Model
{
    public class ExtraCharge
    {
        public int Id { get; set; }

        public ParcelTypeEnum ParcelType { get; set; }

        public float Percentage { get; set; }

        public bool IsSupported { get; set; }
    }
}
