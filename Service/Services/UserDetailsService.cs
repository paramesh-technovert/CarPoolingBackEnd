using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Repository;

namespace CarPoolingWebAPI.Services
{
    public class UserDetailsService
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserDetailsService(CarPoolingDbContext dbContext,IMapper mapper) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<UserDetailsRequestDT> AddUser(UserDetailsRequestDT userDetailsRequestDTO)
        {
            userDetailsRequestDTO.ImageUrl=setImage(userDetailsRequestDTO);
            var userDetail=_mapper.Map<UserDetail>(userDetailsRequestDTO);
            UserDetails userDetails = new UserDetails(_dbContext);
            var result=await userDetails.AddUserDetails(userDetail);
            return _mapper.Map<UserDetail, UserDetailsRequestDT>(result);
        }
        public async Task<UserDetailsRequestDT> GetUserDetails(Guid id)
        {
            UserDetails userDetails = new UserDetails(_dbContext);
            UserDetail userDetail= await userDetails.GetUserDetails(id);
            if (userDetail != null)
            {
                return _mapper.Map<UserDetailsRequestDT>(userDetail);
            }
            return null;
        }
        public string setImage(UserDetailsRequestDT userDetailsRequestDTO)
        {
            string folderPath = "F:/CarPoolingWebAPI/Images";
            string filePath;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                string imagePath = @"F:/Default.png";
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                string defaultImage = "F:/CarPoolingWebAPI/Images/Default";
                using (FileStream stream = new FileStream(defaultImage, FileMode.Create))
                {
                    stream.Write(imageBytes, 0, imageBytes.Length);
                }
            }
            if (userDetailsRequestDTO.ImageUrl != "")
            {

                filePath = Path.Combine(folderPath, userDetailsRequestDTO.Id.ToString());
                byte[] imageBytes = Convert.FromBase64String(userDetailsRequestDTO.ImageUrl);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(imageBytes, 0, imageBytes.Length);
                }

            }
            else
            {
                filePath = Path.Combine(folderPath, "Default");
            }
            return filePath;
        }
        public string GetImage(string path)
        {
                byte[] imagePath = System.IO.File.ReadAllBytes(path);
                string base64String = "data:image/png;base64," + Convert.ToBase64String(imagePath);
                return base64String;
        }
    }
}
