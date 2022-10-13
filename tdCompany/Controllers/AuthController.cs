using Microsoft.AspNetCore.Mvc;
using MovieApp.Entities;
using MovieApp.Message.Auth;
using MovieApp.Message.Response;
using tdCompany.Dto;

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

        [Authorize(UserRole.Admin)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var getUser = await _userRepository.GetById(id);
                return Ok(getUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserForCreationDto user )
        {
            var userr = new User()
            {
               Username = user.Username,
               Password = user.Password,
               Role = UserRole.User
            };

            try
            {
                var createdUser = await _userRepository.CreateUser(userr);
                var response = new Message.Response.UserCreateResponse()
                {
                    Code = 200,
                    Message = "A new user created",
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
