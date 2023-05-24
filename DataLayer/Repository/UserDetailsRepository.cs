using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public class UserDetailsRepository : IUserDetails
    {
        private readonly CarPoolingDbContext _dbContext;
        public UserDetailsRepository(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        public UserDetail GetUserDetail(Guid id)
        {
            return _dbContext.UserDetails.FirstOrDefault(e=>e.Id==id);
        }

        public UserDetailsRequestDBO SetUserDetail(UserDetailsRequestDBO userDetailsDTO)
        {
            UserDetail userDetails=(UserDetail)_dbContext.UserDetails.Where(e=>e.Id==userDetailsDTO.Id);
            userDetails.FirstName = userDetailsDTO.FirstName;
            userDetails.LastName = userDetailsDTO.LastName;
            userDetails.PhoneNumber = userDetailsDTO.PhoneNumber;
            //userDetails.ImageUrl = userDetailsDTO.ImageUrl;
            _dbContext.SaveChanges();
            return userDetailsDTO;
        }
    }
}
