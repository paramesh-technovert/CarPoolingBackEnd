namespace CarPoolingWebAPI.Repository
{
    public interface ICities
    {
        public Task<int> GetCityID(string CityName);
        public Task AddCity(string cityName);
    }
}
