using System.Threading.Tasks;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface IPlayerAccessTokenCache
    {
        Task SaveByPlayerId(long playerId, string token);

        Task<string> GetByPlayerId(long playerId);

        Task DeleteByPlayerId(long playerId);
    }
}