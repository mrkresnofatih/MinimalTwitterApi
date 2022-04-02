using System;
using System.Threading.Tasks;
using MinimalTwitterApi.Repositories.Interface;
using MinimalTwitterApi.Templates;
using StackExchange.Redis;

namespace MinimalTwitterApi.Repositories
{
    public class PlayerAccessTokenCache : CacheTemplate<string>, IPlayerAccessTokenCache
    {
        public PlayerAccessTokenCache(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "PLAYERACCESSTOKEN";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromHours(1);
        }

        public async Task SaveByPlayerId(long playerId, string token)
        {
            var stringPlayerId = playerId.ToString();
            await SaveById(stringPlayerId, token);
        }

        public async Task<string> GetByPlayerId(long playerId)
        {
            var stringPlayerId = playerId.ToString();
            return await GetById(stringPlayerId);
        }

        public async Task DeleteByPlayerId(long playerId)
        {
            var stringPlayerId = playerId.ToString();
            await DeleteById(stringPlayerId);
        }
    }
}