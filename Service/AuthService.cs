using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MinimalTwitterApi.Constants.CustomException;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Repositories;
using MinimalTwitterApi.Service.Interfaces;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Service
{
    public class AuthService : IAuthService
    {
        public AuthService(IMapper mapper, PlayerRepository playerRepository, 
            PlayerPasswordUtility playerPasswordUtility, 
            PlayerAccessTokenUtility playerAccessTokenUtility,
            ILogger<AuthService> logger, PlayerAccessTokenCache playerAccessTokenCache)
        {
            _logger = logger;
            _mapper = mapper;
            _playerAccessTokenUtility = playerAccessTokenUtility;
            _playerRepository = playerRepository;
            _playerPasswordUtility = playerPasswordUtility;
            _playerAccessTokenCache = playerAccessTokenCache;
        }

        private readonly PlayerAccessTokenCache _playerAccessTokenCache;
        private readonly ILogger<AuthService> _logger;
        private readonly PlayerAccessTokenUtility _playerAccessTokenUtility;
        private readonly PlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        private readonly PlayerPasswordUtility _playerPasswordUtility;

        private string _initialImageUrl =
            "https://source.boringavatars.com/beam/120/Maria%20Mitchell?colors=FFAD08,EDD75A,73B06F,0C8F8F,405059";

        public async Task<PlayerGetDto> RegisterNewPlayer(PlayerCreateDto playerCreateDto)
        {
            var foundPlayerWithSameUsername = await _playerRepository.GetRawByUsernameOrDefault(playerCreateDto.Username);
            if (foundPlayerWithSameUsername != null)
            {
                throw new UsernameTakenException();
            }
            
            var newPlayer = _mapper.Map<Player>(playerCreateDto);
            newPlayer.ImageUrl = _initialImageUrl;
            newPlayer.Bio = $"Hello, I'm {newPlayer.Username}!";
            newPlayer.Password = _playerPasswordUtility.DoHash(playerCreateDto.Password);
            return await _playerRepository.AddOne(newPlayer);
        }

        public async Task<PlayerLoginResponseDto> Login(PlayerLoginDto playerLoginDto)
        {
            var foundPlayer = await _playerRepository.GetRawByUsername(playerLoginDto.Username);
            var isPasswordValid = _playerPasswordUtility.IsCompareValid(playerLoginDto.Password, foundPlayer.Password);
            if (!isPasswordValid)
            {
                _logger.LogInformation("Incorrect Password!");
                throw new IncorrectPasswordException();
            }

            var token = _playerAccessTokenUtility.GenerateToken(foundPlayer.PlayerId);
            await _playerAccessTokenCache.SaveByPlayerId(foundPlayer.PlayerId, token);
            var playerGetDto = _mapper.Map<PlayerGetDto>(foundPlayer);
            
            return new PlayerLoginResponseDto
            {
                Player = playerGetDto,
                Token = token
            };
        }
    }
}