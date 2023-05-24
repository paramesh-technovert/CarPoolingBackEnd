using CarPoolingWebAPI.Models;

namespace CarPoolingWebAPI.Repository
{
    public interface ILoginCredentials
    {
        public  Task<LoginDetail> AddUser(LoginDetail loginCredential);
        public Task<LoginDetail?> GetUserDetails(String EmailId);
      
    }
}
