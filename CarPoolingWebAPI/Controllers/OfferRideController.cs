using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OfferRideController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public OfferRideController(CarPoolingDbContext dbContext, IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("OfferedRides")]
        public async Task<List<OfferedRidesDTO>> GetRide([FromHeader] Guid Id)
        {
            OfferedRideService offeredRideService = new(_dbContext, _mapper);
            List<OfferedRidesDT> offeredRidesDT = await offeredRideService.GetOfferedRides(Id);
            return _mapper.Map<List<OfferedRidesDTO>>(offeredRidesDT);
        }
        [HttpPost]
        public async Task<OfferRide> SetRide([FromBody] OfferedRideDTO offeredRide)
        {
            OfferedRideService offeredRides = new(_dbContext, _mapper);
            OfferedRideDT offeredRideDT = new OfferedRideDT();
            offeredRideDT = _mapper.Map<OfferedRideDT>(offeredRide);
            OfferRide res = await offeredRides.OfferRide(offeredRideDT);
            return res;
        }
    }
}
