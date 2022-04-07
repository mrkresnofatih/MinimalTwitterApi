using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Repositories.Interface;
using MinimalTwitterApi.Templates;
using StackExchange.Redis;

namespace MinimalTwitterApi.Repositories
{
    public class ConnectionWithProfilesCache : CacheTemplate<Dictionary<long, ConnectionGetProfileDto>>, IConnectionWithProfilesCache
    {
        public ConnectionWithProfilesCache(IDatabase redisDb) : base(redisDb)
        {
        }

        protected override string GetPrefix()
        {
            return "CONNECTIONWITHPROFILES";
        }

        protected override TimeSpan GetLifeTime()
        {
            return TimeSpan.FromMinutes(5);
        }

        private const string FollowingsKey = "1";
        private const string FollowersKey = "2";

        public async Task SaveFollowings(long playerId, int page, Dictionary<long, ConnectionGetProfileDto> data)
        {
            var formatId = GetFormatIdForFollowings(playerId, page);
            await SaveByFormatId(formatId, data);
        }

        public async Task SaveFollowers(long playerId, int page, Dictionary<long, ConnectionGetProfileDto> data)
        {
            var formatId = GetFormatIdForFollowers(playerId, page);
            await SaveByFormatId(formatId, data);
        }

        public async Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowers(long playerId, int page)
        {
            var formatId = GetFormatIdForFollowers(playerId, page);
            return await GetByFormatId(formatId);
        }

        public async Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowings(long playerId, int page)
        {
            var formatId = GetFormatIdForFollowings(playerId, page);
            return await GetByFormatId(formatId);
        }

        public async Task ClearFollowers(long playerId, int page)
        {
            var formatId = GetFormatIdForFollowers(playerId, page);
            await DeleteByFormatId(formatId);
        }

        public async Task ClearFollowings(long playerId, int page)
        {
            var formatId = GetFormatIdForFollowings(playerId, page);
            await DeleteByFormatId(formatId);
        }

        private string GetFormatIdForFollowings(long playerId, int page)
        {
            return $"PLAYERID<{playerId}>,PAGE<{page}>,TYPE<{FollowingsKey}>";
        }
        
        private static string GetFormatIdForFollowers(long playerId, int page)
        {
            return $"PLAYERID<{playerId}>,PAGE<{page}>,TYPE<{FollowersKey}>";
        }

        private async Task SaveByFormatId(string id, Dictionary<long, ConnectionGetProfileDto> data)
        {
            await SaveById(id, data);
        }
        
        private async Task<Dictionary<long, ConnectionGetProfileDto>> GetByFormatId(string id)
        {
            return await GetById(id);
        }
        
        private async Task DeleteByFormatId(string id)
        {
            await DeleteById(id);
        }
    }
}