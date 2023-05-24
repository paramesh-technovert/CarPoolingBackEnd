using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public class UserDetails
    {
        private readonly CarPoolingDbContext _dbcontext;
        public UserDetails(CarPoolingDbContext dbContext) { 
            _dbcontext = dbContext;
        }
        public async Task<UserDetail> AddUserDetails(UserDetail userDetails)
        {
            UserDetail ?user=_dbcontext.UserDetails.FirstOrDefault(e=>e.Id == userDetails.Id);
            if (user != null)
            {
                user.FirstName = userDetails.FirstName;
                user.LastName = userDetails.LastName;
                user.PhoneNumber = userDetails.PhoneNumber;
                user.ImageUrl = userDetails.ImageUrl;
                _dbcontext.UserDetails.Attach(user);
                await _dbcontext.SaveChangesAsync();
                return user;
            }
            else
            {
                _dbcontext.UserDetails.Add(userDetails);
                await _dbcontext.SaveChangesAsync();
                return userDetails;
            }

        }
        public async Task<UserDetail> GetUserDetails(Guid id)
        {
            var result=_dbcontext.UserDetails.FirstOrDefault(e=>e.Id == id);
            if (result != null)
            {
                return result;
            }
            return null;
        }
    }
}
