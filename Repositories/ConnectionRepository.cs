using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Constants.CustomException;
using MinimalTwitterApi.Contexts;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Repositories.Interface;

namespace MinimalTwitterApi.Repositories
{
    public class ConnectionRepository : IConnectionRepository
    {
        public ConnectionRepository(MtDbContext mtDbContext, IMapper mapper)
        {
            _mtDbContext = mtDbContext;
            _mapper = mapper;
        }

        private readonly MtDbContext _mtDbContext;
        private readonly IMapper _mapper;

        public async Task<Dictionary<long, bool>> GetFollowings(long playerId)
        {
            var followings = await _mtDbContext
                .Connections
                .Where(p => p.FollowerId == playerId && p.ConnectionType == ConnectionTypes.Following)
                .Select(p => p.PlayerId)
                .ToListAsync();

            return followings.ToDictionary(p => p, _ => true);
        }

        public async Task<Dictionary<long, bool>> GetFollowers(long playerId)
        {
            var followers = await _mtDbContext
                .Connections
                .Where(p => p.PlayerId == playerId && p.ConnectionType == ConnectionTypes.Following)
                .Select(p => p.FollowerId)
                .ToListAsync();

            return followers.ToDictionary(p => p, _ => true);
        }

        public async Task<ConnectionGetDto> AddOne(Connection connection)
        {
            connection.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            await _mtDbContext.Connections.AddAsync(connection);
            await _mtDbContext.SaveChangesAsync();

            return await GetOne(connection.ConnectionId);
        }

        public async Task<ConnectionGetDto> GetOne(long connectionId)
        {
            var foundConnection = await _mtDbContext
                .Connections
                .Where(p => p.ConnectionId == connectionId)
                .Select(p => new ConnectionGetDto
                {
                    ConnectionId = p.ConnectionId,
                    PlayerId = p.PlayerId,
                    FollowerId = p.FollowerId,
                    ConnectionType = p.ConnectionType,
                    CreatedAt = p.CreatedAt
                })
                .SingleOrDefaultAsync();

            if (foundConnection == null)
            {
                throw new RecordNotFoundException();
            }

            return foundConnection;
        }

        public async Task<ConnectionGetDto> UpdateOne(Connection connection)
        {
            _mtDbContext.Connections.Update(connection);
            await _mtDbContext.SaveChangesAsync();

            return await GetOne(connection.ConnectionId);
        }

        public async Task<Connection> GetOneByPlayerIdFollowerId(long playerId, long followerId)
        {
            return await _mtDbContext
                .Connections
                .Where(p => p.PlayerId == playerId && p.FollowerId == followerId)
                .SingleOrDefaultAsync();
        }

        public async Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowingsWithProfile(long playerId, int skip, int limit)
        {
            var foundFollowings = await _mtDbContext
                .Connections
                .Where(p => p.FollowerId == playerId && p.ConnectionType == ConnectionTypes.Following)
                .Include(p => p.Player)
                .Select(p => new ConnectionGetProfileDto
                {
                    ConnectionId = p.ConnectionId,
                    ConnectionType = p.ConnectionType,
                    PlayerId = p.PlayerId,
                    FollowerId = p.FollowerId,
                    Player = _mapper.Map<PlayerGetDto>(p.Player),
                    CreatedAt = p.CreatedAt
                })
                .OrderBy(p => p.CreatedAt)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            return foundFollowings.ToDictionary(p => p.PlayerId, p => p);
        }

        public async Task<Dictionary<long, ConnectionGetProfileDto>> GetFollowersWithProfile(long playerId, int skip, int limit)
        {
            var foundFollowings = await _mtDbContext
                .Connections
                .Where(p => p.PlayerId == playerId && p.ConnectionType == ConnectionTypes.Following)
                .Include(p => p.Player)
                .Select(p => new ConnectionGetProfileDto
                {
                    ConnectionId = p.ConnectionId,
                    ConnectionType = p.ConnectionType,
                    PlayerId = p.PlayerId,
                    FollowerId = p.FollowerId,
                    Player = _mapper.Map<PlayerGetDto>(p.Player),
                    CreatedAt = p.CreatedAt
                })
                .OrderBy(p => p.CreatedAt)
                .Skip(skip)
                .Take(limit)
                .ToListAsync();

            return foundFollowings.ToDictionary(p => p.FollowerId, p => p);
        }
    }
}