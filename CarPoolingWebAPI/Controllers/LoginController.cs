using AutoMapper;
using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.DTO;
using CarPoolingWebAPI.Models;
using CarPoolingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CarPoolingWebAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : Controller
    {
        private readonly CarPoolingDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public LoginController(CarPoolingDbContext dbContext,IMapper mapper,IConfiguration configuration) : base()
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _config = configuration;
        }
        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<LoginDetail?>> AddUser([FromBody] LoginCredentialsDTO loginCredential)
        {
            LoginService loginService;
            LoginCredentialsDT loginCredentialsDT=new();
            loginCredentialsDT=_mapper.Map<LoginCredentialsDT>(loginCredential);
            try
            {
                loginService = new LoginService(_dbContext,_mapper);
                return Ok(await loginService.AddUser(loginCredentialsDT));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<ActionResult<LoginCredentialsResponseDTO?>> GetUserDetails([FromBody] LoginCredentialsDTO loginCredentialsDTO)
        {
            LoginService loginService = new LoginService(_dbContext,_mapper);
            LoginCredentialsDT loginCredentialsDT = new();
            loginCredentialsDT = _mapper.Map<LoginCredentialsDT>(loginCredentialsDTO);
            try
            {
                LoginCredentialsResponseDT res=await loginService.GetUserDetails(loginCredentialsDT);
                LoginCredentialsResponseDTO result=new();
                result = _mapper.Map<LoginCredentialsResponseDTO>(res);
                if (result!=null)
                {
                    var token=GenerateJSONWebToken();
                    result.JWT = token;
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}
