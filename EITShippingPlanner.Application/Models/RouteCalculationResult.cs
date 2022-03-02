namespace EITShippingPlanner.Application.Models
{
    public class RouteCalculationResult
    {
        public RouteCalculationResult()
        {
        }

        public RouteCalculationResult(string departure, string destination, string transportationAgency)
        {
            Departure = departure;
            Destination = destination;
            TransportationAgency = transportationAgency;
        }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public string TransportationAgency { get; set; }
    }
}
