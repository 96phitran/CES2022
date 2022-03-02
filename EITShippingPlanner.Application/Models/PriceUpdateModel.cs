namespace EITShippingPlanner.Application.Models
{
    public class PriceUpdateModel
    {
        public float Under10InNovToApr { get; set; }

        public float Under10InMayToOct { get; set; }

        public float From10To50InNovToApr { get; set; }

        public float From10To50InMayToOct { get; set; }

        public float Above50InNovToApr { get; set; }

        public float Above50InMayToOct { get; set; }
    }
}
