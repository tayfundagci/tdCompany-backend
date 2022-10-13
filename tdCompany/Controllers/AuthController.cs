using Microsoft.AspNetCore.Mvc;
using MovieApp.Message.Auth;
using MovieApp.Message.Response;

namespace MovieApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthController(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = new LoginResponse() { Code = 500, Message = "Fail", Token = null };
            try
            {
                var user = await _userRepository.GetByUsernameAndPassword(request.Username,request.Password);
                if(user != null)
                {
                    var token = _jwtService.GenerateToken(user);
                    response.Code = 200;
                    response.Token = token;
                    response.Message = "Success";
                    return Ok(response);
                }
                else
                {
                    response.Code = 401;
                    response.Message = "Invalid Credentials";
                    return Unauthorized(response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
