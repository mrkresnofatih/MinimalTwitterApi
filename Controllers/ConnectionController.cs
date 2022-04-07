using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinimalTwitterApi.Attributes;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Controllers.Interfaces;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Service;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectionController : ControllerBase, IConnectionController
    {
        public ConnectionController(ConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        private readonly ConnectionService _connectionService;

        [HttpGet("getAllFollowings")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<Dictionary<long, ConnectionGetProfileDto>>> GetFollowingsWithProfile(
            [FromHeader(Name = CustomHeaders.AuthenticationHeader)] string token,
            [FromQuery(Name = "page")] int page)
        {
            var res = await _connectionService.GetFollowings(token, page);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("getAllFollowers")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<Dictionary<long, ConnectionGetProfileDto>>> GetFollowersWithProfile(
            [FromHeader(Name = CustomHeaders.AuthenticationHeader)] string token, 
            [FromQuery(Name = "page")] int page)
        {
            var res = await _connectionService.GetFollowers(token, page);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("follow/{targetPlayerId}")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<ConnectionGetDto>> Follow(long targetPlayerId, 
            [FromHeader(Name = CustomHeaders.AuthenticationHeader)] string token)
        {
            var res = await _connectionService.Follow(targetPlayerId, token);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("unfollow/{targetPlayerId}")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<ConnectionGetDto>> Unfollow(long targetPlayerId,
            [FromHeader(Name = CustomHeaders.AuthenticationHeader)] string token)
        {
            var res = await _connectionService.Unfollow(targetPlayerId, token);
            return ResponseHandler.WrapSuccess(res);
        }
    }
}
