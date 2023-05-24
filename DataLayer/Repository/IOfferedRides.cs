using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public interface IOfferedRides
    {
        public Task<OfferRide> GetRideDetails(int RideId);
        public Task<OfferRide> OfferRide(OfferRide offeredRide);
        //public Task<List<OfferRide>> GetOfferedRides(Guid id);
        public Task<List<OfferedRidesDBO>> GetOfferedRides(Guid id);
    }
}
