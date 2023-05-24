using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Context;

namespace CarPoolingWebAPI.Repository
{
    public class Cities : ICities
    {
        private readonly CarPoolingDbContext _dbContext;
        public Cities(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetCityID(string cityName)
        {
            City? city=_dbContext.Cities.FirstOrDefault(city=>city.CityName==cityName)!;
            if(city == null)
            {
                return 0;
            }
            return city.CityId;
        }
        public async Task AddCity(string cityName)
        {
            City city=new City();
            city.CityName=cityName;
            await _dbContext.Cities.AddAsync(city);
            await _dbContext.SaveChangesAsync();
        }
        public String GetCityName(int id)
        {
            return _dbContext.Cities.First(e=>e.CityId==id).CityName;
        }
    }
}
