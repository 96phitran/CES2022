using EITShippingPlanner.Application.Dto;
using EITShippingPlanner.Application.Dto.Api;
using EITShippingPlanner.Application.Interface;
using EITShippingPlanner.Application.Models;
using EITShippingPlanner.Core.Enum;
using EITShippingPlanner.Core.Interface;
using EITShippingPlanner.Core.Model;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EITShippingPlanner.Application.Service
{
    public class RouteCalculationService : IRouteCalculationService
    {
        private readonly IParcelPriceRepository _parcelPriceRepository;
        private readonly IExtraChargeRepository _extraChargeRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly ICargoCenterLocationRepository _cargoCenterLocationRepository;
        private readonly IMemoryCache _memoryCache;
        private const int _hourPerSegment = 12;
        private const string _allShipRouteCacheKey = "allShipRouteCache";
        private const string _allLocationCacheKey = "allLocationCache";
        private const string _allConnectionsCacheKey = "allConnectionCache";

        public RouteCalculationService(IParcelPriceRepository parcelPriceRepository,
                                       IExtraChargeRepository extraChargeRepository,
                                       IRouteRepository routeRepository,
                                       ICargoCenterLocationRepository cargoCenterLocationRepository,
                                       IMemoryCache memoryCache)
        {
            _parcelPriceRepository = parcelPriceRepository;
            _extraChargeRepository = extraChargeRepository;
            _routeRepository = routeRepository;
            _cargoCenterLocationRepository = cargoCenterLocationRepository;
            _memoryCache = memoryCache;
        }

        public async Task<CalculationApiResponse> CalculateSegmentByShipForApi(CalculationApiRequest request)
        {
            try
            {
                var totalTime = request.Segment * 12;
                var startDate = DateTime.ParseExact(request.Date, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                var endDate = startDate.AddHours(totalTime);

                var listBasePrices = await _parcelPriceRepository.GetParcelPrices();

                var extraCharge = await _extraChargeRepository.GetExtraChargeByParcelType(request.ParcelType);

                if (extraCharge == null || !extraCharge.IsSupported)
                {
                    return new CalculationApiResponse(responseCode: 422);
                }

                var totalPrice =
                    CalculateSegmentByShip(listBasePrices, startDate, extraCharge.Percentage, request.Segment, request.Weight);

                return new CalculationApiResponse(totalTime, totalPrice, 200);
            }
            catch (Exception)
            {
                return new CalculationApiResponse(responseCode: 500);
            }
        }

        public async Task<IList<ShipRouteResponse>> GetAllShipRoutes()
        {
            if (_memoryCache.TryGetValue(_allShipRouteCacheKey, out List<ShipRouteResponse> allShipRouteCachedResult))
            {
                if (allShipRouteCachedResult.Count > 0)
                {
                    return allShipRouteCachedResult;
                }
            }

            try
            {
                var routes = await _routeRepository.GetAllRoutesByTransportationType(TransportationType.Ship);

                var result = new List<ShipRouteResponse>();

                if (routes != null && routes.Count > 0)
                {
                    foreach (var route in routes)
                    {
                        result.Add(new ShipRouteResponse(route.FirstLocation.Code,
                            route.SecondLocation.Code, route.NumberOfSegment));
                    }
                }
                else
                {
                    return null;
                }

                _memoryCache.Set(_allShipRouteCacheKey, result, TimeSpan.FromDays(1));
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RouteCalculationPageModel> InitializeRoutePageModel()
        {
            var allLocations = new List<LocationDto>();
            if (_memoryCache.TryGetValue(_allLocationCacheKey, out List<LocationDto> allLocationRouteCachedResult))
            {
                if (allLocationRouteCachedResult.Count > 0)
                {
                    allLocations = allLocationRouteCachedResult;
                }
            }
            else
            {
                var locations = await _cargoCenterLocationRepository.GetCargoLocations();

                allLocations = locations.Select(x => new LocationDto(x.Name, x.Code)).ToList();
                _memoryCache.Set(_allLocationCacheKey, allLocations, TimeSpan.FromDays(1));
            }

            return new RouteCalculationPageModel(allLocations);
        }

        public async Task<RouteCalculationPageModel> FindOptimalRoute(RouteCalculationPageModel request)
        {
            var allLocations = new List<LocationDto>();
            if (_memoryCache.TryGetValue(_allLocationCacheKey, out List<LocationDto> allLocationCachedResult))
            {
                if (allLocationCachedResult.Count > 0)
                {
                    allLocations = allLocationCachedResult;
                }
            }
            else
            {
                var locations = await _cargoCenterLocationRepository.GetCargoLocations();

                allLocations = locations.Select(x => new LocationDto(x.Name, x.Code)).ToList();
                _memoryCache.Set(_allLocationCacheKey, allLocations, TimeSpan.FromDays(1));
            }

            request.Locations = allLocations;

            await FindPossibleRoutes(request);

            return request;
        }

        private async Task<IList<Route>> GetAllConnection()
        {
            if (_memoryCache.TryGetValue(_allConnectionsCacheKey, out List<Route> allConnectionsCachedResult))
            {
                if (allConnectionsCachedResult.Count > 0)
                {
                    return allConnectionsCachedResult;
                }
            }

            var allConnections = await _routeRepository.GetRoutes();
            _memoryCache.Set(_allLocationCacheKey, allConnections, TimeSpan.FromDays(1));

            return allConnections;
        }

        private async Task FindPossibleRoutes(RouteCalculationPageModel request)
        {
            var allConnection = await GetAllConnection();

            var directelyConnected = allConnection
                .Where(x => x.FirstLocation.Name == request.Departure || x.SecondLocation.Name == request.Departure
                    || x.FirstLocation.Name == request.Destination || x.SecondLocation.Name == request.Destination).ToList();

            var listBasePrices = await _parcelPriceRepository.GetParcelPrices();

            var extraCharge = await _extraChargeRepository.GetExtraChargeByParcelType(request.ParcelType);

            float totalPrice = 0;
            float totalTime = 0;

            request.Results = new List<RouteCalculationResult>();

            foreach (var route in directelyConnected)
            {
                switch (route.TransportationType)
                {
                    case TransportationType.Ship:
                        totalPrice += CalculateSegmentByShip(listBasePrices, request.ETD,
                            extraCharge.Percentage, route.NumberOfSegment, request.Weight);
                        totalTime += route.NumberOfSegment * 12;
                        break;
                    case TransportationType.Car:
                        try
                        {
                            GetCarService(request, route, totalPrice, totalTime);
                        }
                        catch (Exception)
                        {
                            totalPrice += 0;
                        }
                        break;
                    case TransportationType.Airplane:
                    default:
                        break;
                }

                request.Results
                    .Add(new RouteCalculationResult(route.FirstLocation.Name, route.SecondLocation.Name, route.TransportationType.ToString()));
            }

            request.TotalPrice = totalPrice;
            request.TotalTime = $"{(int)totalTime/24}D{totalTime%24}H";
        }

        private void GetCarService(RouteCalculationPageModel request, Route route, float totalPrice, float totalTime)
        {
            var url = "http://wa-tl-vn.azurewebsites.net/external/calculate-segment";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.Headers["X-api-key-telstar"] = "b8cbd403-4aab-4426-8a40-b4a6adb5c197";
            httpRequest.ContentType = "application/json";

            var data = new CalculationApiRequest();
            data.Length = request.Length;
            data.Weight = request.Weight;
            data.Segment = route.NumberOfSegment;
            data.Date = request.ETD.ToString("yyyy/MM/dd");
            data.ParcelType = request.ParcelType;

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var mapped = JsonConvert.DeserializeObject<CalculationApiResponse>(result);

                totalPrice += mapped.Price;
                totalTime += mapped.Time;
            }
        }

        private float CalculateSegmentByShip(IList<ParcelPrice> parcelPrices, DateTime startDate,
                                             float extraChargePercentage, int numberOfSegment, float weight)
        {
            float totalPrice = 0;
            var segmentStartDate = startDate;

            for (var i = 0; i < numberOfSegment; i++)
            {
                var currentSegmentBasePrice = parcelPrices
                    .FirstOrDefault(x => CheckMonthInRange(x.StartDate, x.EndDate, segmentStartDate)
                        && x.LowerWeight <= weight && x.UpperWeight >= weight).Price;

                totalPrice += currentSegmentBasePrice * weight * extraChargePercentage / 100;

                segmentStartDate = segmentStartDate.AddHours(_hourPerSegment);
            }

            return totalPrice;
        }

        private bool CheckMonthInRange(DateTime startDate, DateTime endDate, DateTime dateToCheck)
        {
            var startMonth = new DateTime(startDate.Month == 11 ? 2021 : 2022, startDate.Month, 1);
            var endMonth = new DateTime(2022, endDate.Month, 1);
            var checkMonth = new DateTime(2022, dateToCheck.Month, 1);

            return checkMonth.Date >= startMonth.Date && checkMonth.Date <= endMonth.Date;
        }
    }
}
