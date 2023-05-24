using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Repository;

namespace CarPoolingWebAPI.Services
{
    public class StopsService
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public StopsService(CarPoolingDbContext dbContext,IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<MatchedRidesResponseDT>> GetMatchedRides(MatchedRidesRequestDT matchedRidesRequestDTO)
        {
            Stops stops = new Stops(_dbContext);
            UserDetailsService userDetailsService = new UserDetailsService(_dbContext,_mapper);
            MatchedRidesRequestDBO matchedRidesRequestDBO = new MatchedRidesRequestDBO();
            matchedRidesRequestDBO=_mapper.Map<MatchedRidesRequestDBO>(matchedRidesRequestDTO);
            List<MatchedRidesResponseDBO> result = (await stops.GetMatchedRides(matchedRidesRequestDBO)).Where(e=>e.AvailableSeats>=matchedRidesRequestDTO.SeatsRequired).ToList();
            for(int i=0; i<result.Count; i++)
            {
                result[i].Image = userDetailsService.GetImage(result[i].Image);
            }
            List<MatchedRidesResponseDT> re=_mapper.Map<List<MatchedRidesResponseDT>>(result);
            return re;
        }


    }
}
