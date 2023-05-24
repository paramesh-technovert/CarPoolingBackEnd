using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public interface IBookRides
    {
        public BookRideRequestDBO BookRide(BookRideRequestDBO bookRideRequestDTO, int Fair);
        public IQueryable<BookedRidesDBO> GetBookedRides(Guid userId);
        //public IQueryable<BookedRide> GetBookedRides(Guid userId);
    }
}
