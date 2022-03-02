namespace EITShippingPlanner.Application.Dto
{
    public class LocationDto
    {
        public LocationDto(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
