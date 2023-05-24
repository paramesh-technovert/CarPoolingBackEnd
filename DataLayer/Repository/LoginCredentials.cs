using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPoolingWebAPI.Repository
{
    public class LoginCredentials:ILoginCredentials
    {
        private readonly CarPoolingDbContext _dbContext;
        public LoginCredentials(CarPoolingDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }

        public async Task<LoginDetail?> AddUser(LoginDetail loginCredential)
        {
            try
            {
                await _dbContext.LoginDetails.AddAsync(loginCredential);
                await _dbContext.SaveChangesAsync();
            }catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
            }
            return loginCredential;
        }

        public async Task<LoginDetail> GetUserDetails(string EmailId)
        {
            LoginDetail credential=_dbContext.LoginDetails.FirstOrDefault(x=>x.Email.ToLower()==EmailId.ToLower());
            return credential;
        }
    }
}
