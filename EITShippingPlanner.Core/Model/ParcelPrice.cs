using System;

namespace EITShippingPlanner.Core.Model
{
    public class ParcelPrice
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public float LowerWeight { get; set; }

        public float UpperWeight { get; set; }

        public float Price { get; set; }
    }
}
