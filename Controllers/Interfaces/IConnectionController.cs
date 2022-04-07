using System.Collections.Generic;
using System.Threading.Tasks;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Controllers.Interfaces
{
    public interface IConnectionController
    {
        Task<ResponsePayload<Dictionary<long, ConnectionGetProfileDto>>> GetFollowingsWithProfile(string token, int page);

        Task<ResponsePayload<Dictionary<long, ConnectionGetProfileDto>>> GetFollowersWithProfile(string token, int page);

        Task<ResponsePayload<ConnectionGetDto>> Follow(long targetPlayerId, string token);
        
        Task<ResponsePayload<ConnectionGetDto>> Unfollow(long targetPlayerId, string token);
    }
}