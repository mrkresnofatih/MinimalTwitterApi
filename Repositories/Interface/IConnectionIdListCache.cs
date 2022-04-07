using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface IConnectionIdListCache
    {
        Task SaveFollowings(long playerId, Dictionary<long, bool> data);
        
        Task SaveFollowers(long playerId, Dictionary<long, bool> data);

        Task<Dictionary<long, bool>> GetFollowers(long playerId);
        
        Task<Dictionary<long, bool>> GetFollowings(long playerId);

        Task ClearFollowers(long playerId);
        
        Task ClearFollowings(long playerId);
    }
}