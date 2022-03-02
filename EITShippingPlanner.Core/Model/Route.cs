using EITShippingPlanner.Core.Enum;

namespace EITShippingPlanner.Core.Model
{
    public class Route
    {
        public int Id { get; set; }

        public int? FirstLocationId { get; set; }

        public virtual CargoCenterLocation FirstLocation { get; set; }

        public int? SecondLocationId { get; set; }

        public virtual CargoCenterLocation SecondLocation { get; set; }

        public TransportationType TransportationType { get; set; }

        public int NumberOfSegment { get; set; }
    }
}
