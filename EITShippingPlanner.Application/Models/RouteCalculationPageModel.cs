using EITShippingPlanner.Application.Dto;
using EITShippingPlanner.Core.Enum;
using System;
using System.Collections.Generic;
using static EITShippingPlanner.Core.Enum.ParcelType;

namespace EITShippingPlanner.Application.Models
{
    public class RouteCalculationPageModel
    {
        public RouteCalculationPageModel() { }

        public RouteCalculationPageModel(IList<LocationDto> locations)
        {
            Locations = locations;
        }

        public RouteCalculationPageModel(IList<LocationDto> locations, IList<RouteCalculationResult> results) : this(locations)
        {
            Results = results;
        }

        public DateTime ETD { get; set; }

        public ParcelTypeEnum ParcelType { get; set; }

        public IList<LocationDto> Locations { get; set; }

        public string Departure { get; set; }

        public string Destination { get; set; }

        public float Weight { get; set; }

        public float Length { get; set; }

        public OptimizationOption Optimization { get; set; }

        public bool PrioritizeEIT { get; set; } = true;

        public float TotalPrice { get; set; }

        public string TotalTime { get; set; }

        public IList<RouteCalculationResult> Results { get; set; }
    }
}
