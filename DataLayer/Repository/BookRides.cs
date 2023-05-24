using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public class BookRides : IBookRides
    {
        private readonly CarPoolingDbContext _dbContext;
        public BookRides(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public BookRideRequestDBO BookRide(BookRideRequestDBO bookRideRequestDTO,int Fair)
        {
            BookedRide bookedRide=new BookedRide()
            {
                CustomerId=bookRideRequestDTO.Id,
                RideId=bookRideRequestDTO.RideId,
                BoardingStop=bookRideRequestDTO.BoardingStopId,
                DestinationStop=bookRideRequestDTO.DestinationStopId,
                Date=bookRideRequestDTO.DateTime,
                SeatsBooked=bookRideRequestDTO.BookedSeats,
                Fair=Fair
            };
            _dbContext.BookedRides.Add(bookedRide);
            
            return bookRideRequestDTO;

        }
        public IQueryable<BookedRidesDBO> GetBookedRides(Guid userId)
        {
            return from a in (from b in (from k in (from i in ( _dbContext.BookedRides.Where(x=>x.CustomerId==userId)) join j in _dbContext.OfferRides on i.RideId equals j.RideId select new {i.RideId,i.BookingId,i.BoardingStop,i.DestinationStop,i.Date,i.Fair,i.SeatsBooked,j.RideProviderId}) join u in _dbContext.UserDetails on k.RideProviderId equals u.Id select new{ k,userName=u.FirstName.Trim()+" "+u.LastName.Trim(),u.Id,u.ImageUrl}) join s in _dbContext.Cities on b.k.BoardingStop equals s.CityId select new {b,BoardingCity=s.CityName}) join c in _dbContext.Cities on a.b.k.DestinationStop equals c.CityId select (new BookedRidesDBO{RideProviderId=a.b.Id,RideProviderName=a.b.userName,BoardingStop=a.BoardingCity.Trim(),Destination=c.CityName.Trim(),Date=a.b.k.Date,price=a.b.k.Fair,SeatsBooked=a.b.k.SeatsBooked,Image=a.b.ImageUrl });
            //return from c in from a in _dbContext.BookedRides join b in _dbContext.Cities on a.CustomerId equals userId where a.BoardingStop == b.CityId select new{a.RideId, a.CustomerId, a.BookingId, a.Date, a.Fair, a.SeatsBooked, a.DestinationStop,b.CityName} join d in _dbContext.Cities on c.DestinationStop equals d.CityId select (new BookingDetailsDTO { RideId=c.RideId,CustomerId=c.CustomerId,BookingId=c.BookingId,Fair=c.Fair,SeatsBooked=c.SeatsBooked,Date=c.Date,TotalFair=c.Fair*c.SeatsBooked,BoardingStop=c.CityName,DestinationStop=d.CityName});
        }
        /*public IQueryable<BookedRide> GetBookedRides(Guid userId)
        {
            return _dbContext.BookedRides.Where(x=>x.CustomerId==userId).AsQueryable();
        }*/
    }
}
