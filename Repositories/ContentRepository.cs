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
    public class ContentRepository : IContentRepository
    {
        public ContentRepository(MtDbContext mtDbContext, ILogger<ContentRepository> logger, 
            IMapper mapper)
        {
            _mtDbContext = mtDbContext;
            _logger = logger;
            _mapper = mapper;
        }

        private readonly MtDbContext _mtDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ContentRepository> _logger;

        public async Task<long> AddOne(Content content)
        {
            content.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            await _mtDbContext.Contents.AddAsync(content);
            await _mtDbContext.SaveChangesAsync();

            return content.ContentId;
        }

        public async Task<ContentGetDto> GetOne(long contentId)
        {
            var foundContent = await _mtDbContext
                .Contents
                .Where(p => p.ContentId == contentId)
                .Include(p => p.Tweet)
                .ThenInclude(p => p.Player)
                .Select(p => new ContentQueryDto
                {
                    ContentId = p.ContentId,
                    ContentType = p.ContentType,
                    Message = p.Message,
                    ImageUrl = p.ImageUrl,
                    CreatedAt = p.CreatedAt,
                    Player = p.Player,
                    Tweet = p.Tweet,
                    LikesCount = p
                        .Tweet
                        .Contents
                        .Count(q => q.ContentType == ContentTypes.Like),
                    RepliesCount = p
                        .Tweet
                        .Contents
                        .Count(q => q.ContentType == ContentTypes.Reply)
                })
                .SingleOrDefaultAsync();

            if (foundContent == null)
            {
                _logger.LogInformation("Content Not Found!");
                throw new RecordNotFoundException();
            }

            return new ContentGetDto
            {
                ContentId = foundContent.ContentId,
                ContentType = foundContent.ContentType,
                Message = foundContent.Message,
                ImageUrl = foundContent.ImageUrl,
                CreatedAt = foundContent.CreatedAt,
                Player = _mapper.Map<PlayerGetDto>(foundContent.Player),
                Tweet = _mapper.Map<TweetGetDto>(foundContent.Tweet),
                LikesCount = foundContent.LikesCount,
                RepliesCount = foundContent.RepliesCount
            };
        }
    }
}