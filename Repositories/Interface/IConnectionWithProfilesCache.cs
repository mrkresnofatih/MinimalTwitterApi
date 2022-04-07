using System.Collections.Generic;
using System.Threading.Tasks;
using MinimalTwitterApi.Models;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface IConnectionWithProfilesCache
    {
        Task SaveFollowings(long playerId, int page, Dictionary<long, ConnectionGetProfileDto> data);
        
        Task SaveFollowers(long playerId, int page, Dictionary<long, ConnectionGetProfileDto> data);
        
        Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowers(long playerId, int page);
        
        Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowings(long playerId, int page);
        
        Task ClearFollowers(long playerId, int page);
        
        Task ClearFollowings(long playerId, int page);
    }
}