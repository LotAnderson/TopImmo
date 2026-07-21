using Microsoft.AspNetCore.Mvc;
using Model;
using System.Runtime.InteropServices;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserLoginService _loginService;

        public AuthController(UserLoginService loginService)
        {
            _loginService = loginService;
        }

        public class LoginRequest
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (_loginService.ValidateCredentials(request.Username, request.Password))
            {
                var sid = _loginService.GenerateSessionId();
                return Ok(new { sessionId = sid });
            }
            return Unauthorized(new { message = "Invalid credentials" });
        }
    }
}