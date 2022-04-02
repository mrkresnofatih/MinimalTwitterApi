using System.Threading.Tasks;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Controllers.Interfaces
{
    public interface IAuthController
    {
        Task<ResponsePayload<PlayerGetDto>> Register(PlayerCreateDto playerCreateDto);

        Task<ResponsePayload<PlayerLoginResponseDto>> Login(PlayerLoginDto playerLoginDto);
    }
}