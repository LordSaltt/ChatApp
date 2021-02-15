using System.Threading.Tasks;
using API.Dtos;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var response = await _service.Login(userForLoginDto);

            if(response == null)
                return Unauthorized();

            return Ok(new
            {
                response
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var createdUser = await _service.Register(userForRegisterDto);
            
            if(createdUser == null)
                return BadRequest("User Already Exist");

            return StatusCode(201);
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _service.GetUsers();

            return Ok(new
            {
                response
            });
        }

    }
}