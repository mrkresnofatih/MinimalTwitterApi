using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Constants.CustomException;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Models.Factories;
using MinimalTwitterApi.Repositories;
using MinimalTwitterApi.Service.Interfaces;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Service
{
    public class ConnectionService : IConnectionService
    {
        public ConnectionService(ConnectionRepository connectionRepository, 
            PlayerAccessTokenUtility playerAccessTokenUtility, ILogger<ConnectionRepository> logger,
            ConnectionWithProfilesCache connectionWithProfilesCache,
            ConnectionIdListCache connectionIdListCache)
        {
            _connectionRepository = connectionRepository;
            _logger = logger;
            _playerAccessTokenUtility = playerAccessTokenUtility;
            _connectionWithProfilesCache = connectionWithProfilesCache;
            _connectionIdListCache = connectionIdListCache;
        }

        private readonly ConnectionIdListCache _connectionIdListCache;
        private readonly ConnectionWithProfilesCache _connectionWithProfilesCache;
        private readonly ILogger<ConnectionRepository> _logger;
        private readonly PlayerAccessTokenUtility _playerAccessTokenUtility;
        private readonly ConnectionRepository _connectionRepository;

        private int _connectionWithProfilesLimit = 20;

        public async Task<ConnectionGetDto> Follow(long targetId, string token)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);

            var foundConn = await _connectionRepository
                .GetOneByPlayerIdFollowerId(targetId, playerId);

            if (foundConn == null)
            {
                var connectionDraft = ConnectionFactory.BuildNewConnectionFollowing(targetId, playerId);
                return await _connectionRepository.AddOne(connectionDraft);
            }

            if (foundConn.ConnectionType == ConnectionTypes.Following)
            {
                _logger.LogInformation("Record Found & Already Following!");
                throw new AlreadyFollowingException();
            }

            foundConn.ConnectionType = ConnectionTypes.Following;
            return await _connectionRepository.UpdateOne(foundConn);
        }

        public async Task<ConnectionGetDto> Unfollow(long targetId, string token)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);
            
            var foundConn = await _connectionRepository
                .GetOneByPlayerIdFollowerId(targetId, playerId);
            
            if (foundConn == null)
            {
                _logger.LogInformation("Record Not Found, Already Not Following!");
                throw new AlreadyNotFollowingException();
            }

            foundConn.ConnectionType = ConnectionTypes.NotFollowing;
            return await _connectionRepository.UpdateOne(foundConn);
        }

        public async Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowers(string token, int page)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);

            var followersFromCache = await _connectionWithProfilesCache
                .GetFollowers(playerId, page);

            if (followersFromCache != null)
            {
                return followersFromCache;
            }

            var paginationCalculation = PaginationUtility
                .CalculatePagination(page, _connectionWithProfilesLimit);
            var resultFromDb = await _connectionRepository
                .GetFollowersWithProfile(
                    playerId, 
                    paginationCalculation.QuerySkip, 
                    paginationCalculation.QueryLimit);

            await _connectionWithProfilesCache.SaveFollowers(playerId, page, resultFromDb);
            return resultFromDb;
        }

        public async Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowings(string token, int page)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);

            var followingsFromCache = await _connectionWithProfilesCache
                .GetFollowings(playerId, page);

            if (followingsFromCache != null)
            {
                return followingsFromCache;
            }
            
            var paginationCalculation = PaginationUtility
                .CalculatePagination(page, _connectionWithProfilesLimit);
            var resultFromDb = await _connectionRepository
                .GetFollowingsWithProfile(
                    playerId, 
                    paginationCalculation.QuerySkip, 
                    paginationCalculation.QueryLimit);

            await _connectionWithProfilesCache.SaveFollowings(playerId, page, resultFromDb);
            return resultFromDb;
        }

        public async Task<Dictionary<long, bool>> GetFollowingsIds(string token)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);

            var resultFromCache = await _connectionIdListCache.GetFollowings(playerId);

            if (resultFromCache != null)
            {
                return resultFromCache;
            }
            
            var resultFromDb = await _connectionRepository.GetFollowings(playerId);
            await _connectionIdListCache.SaveFollowings(playerId, resultFromDb);
            return resultFromDb;
        }

        public async Task<Dictionary<long, bool>> GetFollowersIds(string token)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);
            
            var resultFromCache = await _connectionIdListCache.GetFollowers(playerId);

            if (resultFromCache != null)
            {
                return resultFromCache;
            }
            var resultFromDb = await _connectionRepository.GetFollowers(playerId);
            await _connectionIdListCache.SaveFollowers(playerId, resultFromDb);
            return resultFromDb;
        }
    }
}