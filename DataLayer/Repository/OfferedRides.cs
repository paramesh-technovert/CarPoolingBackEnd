using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingWebAPI.Repository
{
    public class OfferedRides : IOfferedRides
    {
        private readonly CarPoolingDbContext _dbContext;
        public OfferedRides(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        /*public async Task<List<OfferRide>> GetOfferedRides(Guid id)
        {
            return  _dbContext.OfferRides.Where(e=>e.RideProviderId==id).ToList();
        }*/
        public async Task<List<OfferedRidesDBO>> GetOfferedRides(Guid id)
        {
            return (from j in 
                        (from i in 
                             (from ob in 
                                  (from o in _dbContext.OfferRides
                                   .Where(e=>e.RideProviderId==id) 
                                   join b in _dbContext.BookedRides 
                                   on o.RideId equals b.RideId select b) 
                              join u in _dbContext.UserDetails 
                              on ob.CustomerId equals u.Id 
                              select new{ ob,
                                  Image=u.ImageUrl,
                                  userName=u.FirstName.Trim()+" "+u.LastName.Trim()}) 
                         join c in _dbContext.Cities 
                         on i.ob.BoardingStop equals c.CityId 
                         select new {i,BoardingStop=c.CityName}) 
                    join s in _dbContext.Cities 
                    on j.i.ob.DestinationStop equals s.CityId 
                    select (
                    new OfferedRidesDBO {
                        CustomerId=j.i.ob.CustomerId,
                        CustomerName=j.i.userName,
                        Image=j.i.Image,
                        BoardingStop=j.BoardingStop,
                        Destination=s.CityName,
                        price=j.i.ob.Fair,
                        SeatsBooked=j.i.ob.SeatsBooked,
                        Date=j.i.ob.Date
                    })).ToList();
            //return (from c in from a in _dbContext.OfferRides join b in _dbContext.Cities on a.RideProviderId equals id where a.SourceId == b.CityId select new { a.RideId,a.RideProviderId, a.Date, a.TotalSeats, a.Fair,a.Distance, a.DestinationId, b.CityName } join d in _dbContext.Cities on c.DestinationId equals d.CityId select (new OfferedRidesResponseDTO{ RideId=c.RideId,RideOwnerId=c.RideProviderId,Date=c.Date,Distance=c.Distance,Fair=c.Fair,TotalSeats=c.TotalSeats,StartingStop=c.CityName,EndingStop=d.CityName})).ToList();
        }
        public async Task<OfferRide?> GetRideDetails(int RideId)
        {
            return await _dbContext.OfferRides.FindAsync(RideId);
        }

        public async Task<OfferRide> OfferRide(OfferRide offeredRide)
        {
            try
            {
                await _dbContext.OfferRides.AddAsync(offeredRide);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
            }
            return offeredRide;
        }
    }
}
