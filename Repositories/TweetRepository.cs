using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Constants.CustomException;
using MinimalTwitterApi.Contexts;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Models.Content;
using MinimalTwitterApi.Repositories.Interface;

namespace MinimalTwitterApi.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        public TweetRepository(MtDbContext mtDbContext, ILogger<TweetRepository> logger, IMapper mapper)
        {
            _mtDbContext = mtDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        private readonly IMapper _mapper;
        private readonly MtDbContext _mtDbContext;
        private readonly ILogger<TweetRepository> _logger;

        public async Task<long> AddOne(Tweet tweet)
        {
            tweet.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            await _mtDbContext.Tweets.AddAsync(tweet);
            await _mtDbContext.SaveChangesAsync();

            return tweet.TweetId;
        }

        public async Task<TweetGetDto> GetOne(long tweetId)
        {
            var foundTweet = await _mtDbContext
                .Tweets
                .Where(p => p.TweetId == tweetId)
                .Select(p => new TweetGetDto
                {
                    TweetId = p.TweetId,
                    Message = p.Message,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt,
                    Player = _mapper.Map<PlayerGetDto>(p.Player)
                })
                .SingleOrDefaultAsync();
            
            if (foundTweet == null)
            {
                _logger.LogInformation("Tweet Not Found!");
                throw new RecordNotFoundException();
            }

            return foundTweet;
        }

        public async Task<bool> LikeExists(long tweetId, long playerId)
        {
            return await _mtDbContext
                .Contents
                .Where(p => 
                    p.TweetId == tweetId && 
                    p.PlayerId == playerId && 
                    p.ContentType == ContentTypes.Like)
                .Select(_ => true)
                .SingleOrDefaultAsync();
        }
    }
}