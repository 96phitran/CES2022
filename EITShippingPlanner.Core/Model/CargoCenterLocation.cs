using System.Collections.Generic;

namespace EITShippingPlanner.Core.Model
{
    public class CargoCenterLocation
    {
        public CargoCenterLocation() {}

        public CargoCenterLocation(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public virtual IList<Route> RouteFrom { get; set; }

        public virtual IList<Route> RouteTo { get; set; }
    }
}
