using System.Threading.Tasks;
using MinimalTwitterApi.Models;

namespace MinimalTwitterApi.Service.Interfaces
{
    public interface IAuthService
    {
        Task<PlayerGetDto> RegisterNewPlayer(PlayerCreateDto playerCreateDto);

        Task<PlayerLoginResponseDto> Login(PlayerLoginDto playerLoginDto);
    }
}