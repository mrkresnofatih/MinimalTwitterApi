using System.Collections.Generic;
using System.Threading.Tasks;
using MinimalTwitterApi.Models;

namespace MinimalTwitterApi.Service.Interfaces
{
    public interface IConnectionService
    {
        Task<ConnectionGetDto> Follow(long targetId, string token);

        Task<ConnectionGetDto> Unfollow(long targetId, string token);

        Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowers(string token, int page);
        
        Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowings(string token, int page);

        Task<Dictionary<long, bool>> GetFollowingsIds(string token);
        
        Task<Dictionary<long, bool>> GetFollowersIds(string token);
    }
}