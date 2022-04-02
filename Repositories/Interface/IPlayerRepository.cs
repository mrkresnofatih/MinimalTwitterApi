using System.Threading.Tasks;
using MinimalTwitterApi.Models;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface IPlayerRepository
    {
        Task<PlayerGetDto> AddOne(Player playerDraft);

        Task<PlayerGetDto> GetOne(long playerId);

        Task<Player> GetRawByUsername(string username);

        Task<Player> GetRawByUsernameOrDefault(string username);
    }
}