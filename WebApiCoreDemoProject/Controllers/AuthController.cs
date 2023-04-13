using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreDemoProject.Models.DTO;
using WebApiCoreDemoProject.Repositories.IRepository;

namespace WebApiCoreDemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {   
            //validate incoming request
            //check if user is authenticate
            //check the username and password
            var isAuthenticatedUser = await _userRepository.AuthenticateUserAsync(loginRequest.Username,loginRequest.Password);
            if(isAuthenticatedUser != null)
            {
                //generate JWT
               var token = await _tokenHandler.CreateTokenAsync(isAuthenticatedUser);
                return Ok(token);
     
            }
            return BadRequest("Username and Password is incorrect");
        }

        [HttpPost]
        public async Task<IActionResult> GetUserRolesAsync(LoginRequest loginRequest)
        {
            var roles = await _userRepository.GetUserRolesAsync(loginRequest.Username, loginRequest.Password);
            if (roles != null)
            {
                return Ok(roles);
            }
            return BadRequest();
        }

    }
}
