using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;

namespace CarPoolingWebAPI.Services
{
    public class BookRideService
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookRideService(CarPoolingDbContext dbContext,IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<BookRideResponseDT> BookRide(BookRideRequestDT bookingRideRequestDTO)
        {
            OfferedRides offeredRide = new OfferedRides(_dbContext);
            Cities cities = new Cities(_dbContext);
            Stops stops = new Stops(_dbContext); 
            BookRideRequestDBO bookRideRequestDBO = new BookRideRequestDBO();
            bookRideRequestDBO = _mapper.Map<BookRideRequestDBO>(bookingRideRequestDTO);
            stops.BookingRide(bookRideRequestDBO);
            bookingRideRequestDTO.BoardingStopId = stops.GetStopId(bookingRideRequestDTO.BoardingStopId);
            bookingRideRequestDTO.DestinationStopId = stops.GetStopId(bookingRideRequestDTO.DestinationStopId);
            OfferRide? offerRide = await offeredRide.GetRideDetails(bookingRideRequestDTO.RideId);
            var bookingDetailsDTO = _mapper.Map<BookRideRequestDT, BookRideResponseDT>(bookingRideRequestDTO);
            _mapper.Map(offerRide,bookingDetailsDTO);
            bookingDetailsDTO.TotalAmount = offerRide!.Fair * bookingRideRequestDTO.BookedSeats;
            bookingDetailsDTO.BoardingPoint = cities.GetCityName(bookingRideRequestDTO.BoardingStopId);
            bookingDetailsDTO.DroppingPoint = cities.GetCityName(bookingRideRequestDTO.DestinationStopId);
            BookRides bookRides = new BookRides(_dbContext);
            BookRideRequestDBO bookRideDBO = new BookRideRequestDBO();
            bookRideRequestDBO = _mapper.Map<BookRideRequestDBO>(bookingRideRequestDTO);
            try
            {

                bookRides.BookRide(bookRideRequestDBO, offerRide.Fair);
                _dbContext.SaveChanges();
            }catch (Exception ex)
            {
                throw new Exception("Couldn't Book a ride Please Try Again");
            }
            return bookingDetailsDTO;
        }
        public List<BookedRidesDBO> GetBookedRides(Guid userId)
        {
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext,_mapper);
            BookRides bookRides = new BookRides(_dbContext);
            var result=bookRides.GetBookedRides(userId).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].Image != null)
                    result[i].Image = userDetailsService.GetImage(result[i].Image);
            }
            return result.ToList();
        }
        /*public List<BookingDetailsDTO> GetBookedRides(Guid userId)
        {
            Cities cities = new Cities(_dbContext);
            BookRides bookRides = new BookRides(_dbContext);
            IQueryable<BookedRide> rides= bookRides.GetBookedRides(userId);
            List<BookingDetailsDTO> bookingDetails=new List<BookingDetailsDTO>();
            foreach(BookedRide ride in rides)
            {
                BookingDetailsDTO bookingDetailsDTO = new BookingDetailsDTO()
                {
                    RideId = ride.RideId,
                    BookingId = ride.BookingId,
                    CustomerId = ride.CustomerId,
                    SeatsBooked = ride.SeatsBooked,
                    Fair = ride.Fair,
                    TotalFair = ride.Fair * ride.SeatsBooked,
                    Date = ride.Date,
                    BoardingStop = cities.GetCityName(ride.BoardingStop),
                    DestinationStop = cities.GetCityName(ride.DestinationStop)
                };
                bookingDetails.Add(bookingDetailsDTO);
            }
            return bookingDetails;
        }*/
    }
}
