using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinimalTwitterApi.Repositories.Interface;
using MinimalTwitterApi.Templates;
using StackExchange.Redis;

namespace MinimalTwitterApi.Repositories
{
    public class ConnectionIdListCache : CacheTemplate<Dictionary<long, bool>>, IConnectionIdListCache
    {
        public ConnectionIdListCache(IDatabase redisDb) : base(redisDb)
        {
        }

        private const string FollowingsKey = "1";
        private const string FollowersKey = "2";

        protected override string GetPrefix()
        {
            return "CONNECTIONIDLIST";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromHours(1);
        }

        public async Task SaveFollowings(long playerId, Dictionary<long, bool> data)
        {
            var id = GetFormatIdForFollowings(playerId);
            await SaveById(id, data);
        }

        public async Task SaveFollowers(long playerId, Dictionary<long, bool> data)
        {
            var id = GetFormatIdForFollowers(playerId);
            await SaveById(id, data);
        }

        public async Task<Dictionary<long, bool>> GetFollowers(long playerId)
        {
            var id = GetFormatIdForFollowers(playerId);
            return await GetById(id);
        }

        public async Task<Dictionary<long, bool>> GetFollowings(long playerId)
        {
            var id = GetFormatIdForFollowings(playerId);
            return await GetById(id);
        }

        public async Task ClearFollowers(long playerId)
        {
            var id = GetFormatIdForFollowers(playerId);
            await DeleteById(id);
        }

        public async Task ClearFollowings(long playerId)
        {
            var id = GetFormatIdForFollowings(playerId);
            await DeleteById(id);
        }

        private static string GetFormatIdForFollowers(long playerId)
        {
            return $"PLAYERID<{playerId}>,TYPE<{FollowersKey}>";
        }
        
        private static string GetFormatIdForFollowings(long playerId)
        {
            return $"PLAYERID<{playerId}>,TYPE<{FollowingsKey}>";
        }
    }
}