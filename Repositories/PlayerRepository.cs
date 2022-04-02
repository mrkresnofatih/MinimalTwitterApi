using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinimalTwitterApi.Constants.CustomException;
using MinimalTwitterApi.Contexts;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Repositories.Interface;

namespace MinimalTwitterApi.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        public PlayerRepository(MtDbContext mtDbContext, ILogger<PlayerRepository> logger)
        {
            _mtDbContext = mtDbContext;
            _logger = logger;
        }

        private readonly ILogger<PlayerRepository> _logger;
        private readonly MtDbContext _mtDbContext;

        public async Task<PlayerGetDto> AddOne(Player playerDraft)
        {
            playerDraft.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await _mtDbContext.Players.AddAsync(playerDraft);
            await _mtDbContext.SaveChangesAsync();

            return await GetOne(playerDraft.PlayerId);
        }

        public async Task<PlayerGetDto> GetOne(long playerId)
        {
            var foundPlayer = await _mtDbContext
                .Players
                .Where(p => p.PlayerId == playerId)
                .Select(p => new PlayerGetDto
                {
                    PlayerId = p.PlayerId,
                    Username = p.Username,
                    Fullname = p.Fullname,
                    Bio = p.Bio,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt
                })
                .SingleOrDefaultAsync();

            if (foundPlayer == null)
            {
                _logger.LogInformation("Player Not Found");
                throw new RecordNotFoundException();
            }

            return foundPlayer;
        }

        public async Task<Player> GetRawByUsername(string username)
        {
            var foundPlayer = await GetRawByUsernameOrDefault(username);
            if (foundPlayer == null)
            {
                _logger.LogInformation("Player Not Found");
                throw new RecordNotFoundException();
            }

            return foundPlayer;
        }

        public async Task<Player> GetRawByUsernameOrDefault(string username)
        {
            return await _mtDbContext
                .Players
                .Where(p => p.Username == username)
                .SingleOrDefaultAsync();
        }
    }
}