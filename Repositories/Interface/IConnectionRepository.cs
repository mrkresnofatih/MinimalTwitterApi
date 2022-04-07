using System.Collections.Generic;
using System.Threading.Tasks;
using MinimalTwitterApi.Models;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface IConnectionRepository
    {
        Task<Dictionary<long, bool>> GetFollowings(long playerId);
        
        Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowingsWithProfile(long playerId, int skip, int limit);

        Task<Dictionary<long, bool>> GetFollowers(long playerId);
        
        Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowersWithProfile(long playerId, int skip, int limit);

        Task<ConnectionGetDto> AddOne(Connection connection);

        Task<ConnectionGetDto> UpdateOne(Connection connection);

        Task<ConnectionGetDto> GetOne(long connectionId);

        Task<Connection> GetOneByPlayerIdFollowerId(long playerId, long followerId);
    }
}