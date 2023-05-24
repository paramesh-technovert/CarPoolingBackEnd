using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/MatchedRides")]
    [Authorize]
    public class StopsController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public StopsController(CarPoolingDbContext dbContext, IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<List<MatchedRidesResponseDTO>> GetMatchedRides([FromBody] MatchedRidesRequestDTO matchedRidesRequestDTO)
        {
            StopsService service = new StopsService(_dbContext, _mapper);
            MatchedRidesRequestDT matchedRidesRequestDT = new MatchedRidesRequestDT();
            matchedRidesRequestDT = _mapper.Map<MatchedRidesRequestDT>(matchedRidesRequestDTO);
            List<MatchedRidesResponseDT> res = await service.GetMatchedRides(matchedRidesRequestDT);
            return _mapper.Map<List<MatchedRidesResponseDTO>>(res);
        }
    }
}
