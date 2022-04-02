using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinimalTwitterApi.Attributes;
using MinimalTwitterApi.Controllers.Interfaces;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Service;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase, IAuthController
    {
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        private readonly AuthService _authService;

        [HttpPost("register")]
        public async Task<ResponsePayload<PlayerGetDto>> Register([FromBody] PlayerCreateDto playerCreateDto)
        {
            var player = await _authService.RegisterNewPlayer(playerCreateDto);
            return ResponseHandler.WrapSuccess(player);
        }

        [HttpPost("login")]
        public async Task<ResponsePayload<PlayerLoginResponseDto>> Login([FromBody] PlayerLoginDto playerLoginDto)
        {
            var token = await _authService.Login(playerLoginDto);
            return ResponseHandler.WrapSuccess(token);
        }

        [HttpGet("ping")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public ResponsePayload<string> Ping()
        {
            return ResponseHandler.WrapSuccess("pong");
        }
    }
}