using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class BookRideController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public BookRideController(CarPoolingDbContext dbContext, IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<BookRideResponseDTO> BookRide([FromBody] BookRideRequestDTO bookRideRequestDTO)
        {
            if (bookRideRequestDTO.AvailableSeats < bookRideRequestDTO.BookedSeats)
            {
                throw new Exception("Execeded maximum available seats");
            }
            BookRideService bookRideService = new BookRideService(_dbContext, _mapper);
            BookRideRequestDT bookRideRequestDT = new BookRideRequestDT();
            bookRideRequestDT = _mapper.Map<BookRideRequestDT>(bookRideRequestDTO);
            BookRideResponseDT res = await bookRideService.BookRide(bookRideRequestDT);
            return _mapper.Map<BookRideResponseDTO>(res);
        }
        [HttpGet]
        public List<BookedRidesDTO> GetBookedRides([FromHeader] Guid userId)
        {
            BookRideService bookRideService = new BookRideService(_dbContext, _mapper);
            List<BookedRidesDBO> bookedRidesDBO = bookRideService.GetBookedRides(userId);
            return _mapper.Map<List<BookedRidesDTO>>(bookedRidesDBO);
        }
    }
}
