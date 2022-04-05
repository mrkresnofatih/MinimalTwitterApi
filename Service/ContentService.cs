using System.Threading.Tasks;
using MinimalTwitterApi.Models.Content;
using MinimalTwitterApi.Models.Factories;
using MinimalTwitterApi.Repositories;
using MinimalTwitterApi.Service.Interfaces;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Service
{
    public class ContentService : IContentService
    {
        public ContentService(ContentRepository contentRepository, 
            PlayerAccessTokenUtility playerAccessTokenUtility,
            TweetRepository tweetRepository)
        {
            _contentRepository = contentRepository;
            _tweetRepository = tweetRepository;
            _playerAccessTokenUtility = playerAccessTokenUtility;
        }

        private readonly PlayerAccessTokenUtility _playerAccessTokenUtility;
        private readonly ContentRepository _contentRepository;
        private readonly TweetRepository _tweetRepository;

        public async Task<ContentGetDto> AddOriginal(ContentCreateDto contentCreateDto, string token)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);
            
            var tweetDraft = ContentFactory
                .BuildNewTweet(contentCreateDto.Message, contentCreateDto.ImageUrl, playerId);
            var newTweetId = await _tweetRepository.AddOne(tweetDraft);
            
            var contentDraft = ContentFactory
                .BuildNewContentOriginal(playerId, newTweetId);
            var newContentId = await _contentRepository.AddOne(contentDraft);

            return await GetOne(newContentId);
        }

        public async Task<ContentGetDto> AddLike(long tweetId, string token)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);

            var contentDraft = ContentFactory.BuildNewContentLike(playerId, tweetId);
            var newContentId = await _contentRepository.AddOne(contentDraft);

            return await GetOne(newContentId);
        }

        public async Task<ContentGetDto> AddReply(ContentCreateDto contentCreateDto, 
            long tweetId, string token)
        {
            var playerId = _playerAccessTokenUtility.GetPlayerIdFromToken(token);

            var contentDraft = ContentFactory.BuildNewContentReply(contentCreateDto, playerId, tweetId);
            var newContentId = await _contentRepository.AddOne(contentDraft);

            return await GetOne(newContentId);
        }

        public async Task<ContentGetDto> GetOne(long contentId)
        {
            return await _contentRepository.GetOne(contentId);
        }
    }
}