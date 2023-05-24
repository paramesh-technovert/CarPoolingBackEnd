using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public interface IStops
    {
        public Task AddStops(Stop stop);
        public Task<List<MatchedRidesResponseDBO>> GetMatchedRides(MatchedRidesRequestDBO matchedRidesRequestDTO);
        public void BookingRide(BookRideRequestDBO bookRideRequestDTO);
    }
}
