using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;

namespace CarPoolingWebAPI.Services
{
    public class OfferedRideService
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public OfferedRideService(CarPoolingDbContext dbContext,IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        /*public Task<List<OfferRide>> GetOfferedRides(Guid id)
        {
            OfferedRides offeredRides = new(_dbContext);
            return offeredRides.GetOfferedRides(id);
        }*/
        public async Task<List<OfferedRidesDT>> GetOfferedRides(Guid id)
        {
            OfferedRides offeredRides = new(_dbContext);
            UserDetailsService service = new UserDetailsService(_dbContext,_mapper);
            var result=await offeredRides.GetOfferedRides(id);
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Image!=null)
                result[i].Image = service.GetImage(result[i].Image);
            }
            List<OfferedRidesDT> re = _mapper.Map<List<OfferedRidesDT>>(result);
            return re;
        }
        public async Task<OfferRide> OfferRide(OfferedRideDT offeredRide)
        {
            Cities cities = new Cities(_dbContext);
            string city = offeredRide.StartingStop.ToLower().Trim();
            int sourceId = await cities.GetCityID(city);
            if (sourceId == 0)
            {
                await cities.AddCity(city);
                sourceId = await cities.GetCityID(city);
            }
            city = offeredRide.EndingStop.ToLower().Trim();
            int destinationId = await cities.GetCityID(city);
            if (destinationId == 0)
            {
                await cities.AddCity(city);
                destinationId = await cities.GetCityID(city);
            }
            var ride = _mapper.Map<OfferedRideDT, OfferRide>(offeredRide);
            ride.SourceId= sourceId;
            ride.DestinationId= destinationId;
            Stops stops = new Stops(_dbContext);
            OfferedRides offeredRides = new OfferedRides(_dbContext);
            OfferRide rideDetails = await offeredRides.OfferRide(ride);
            for (int i = 0; i < offeredRide.stops.Count(); i++)
            {
                Stop stop = new Stop();
                if (i != 0 && i != offeredRide.stops.Count() - 1)
                {
                    if (offeredRide.stops[i].PickupDate  < offeredRide.stops[i - 1].PickupDate)
                    {
                        throw new Exception("Invalid date and time entered. Please enter valid date and time");
                    }
                }
                stop.RideId = rideDetails.RideId;
                stop.StopId = await cities.GetCityID(offeredRide.stops[i].StopName.ToLower().Trim());
                if (stop.StopId == 0)
                {
                    await cities.AddCity(offeredRide.stops[i].StopName.ToLower().Trim());
                    stop.StopId = await cities.GetCityID(offeredRide.stops[i].StopName.ToLower().Trim());
                }
                stop.AvailableSeats = offeredRide.TotalSeats;
                stop.Date = offeredRide.stops[i].PickupDate;
                await stops.AddStops(stop);

            }
            await _dbContext.SaveChangesAsync();
            return rideDetails;
        }
    }
}
