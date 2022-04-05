using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Models.Factories
{
    public static class ContentFactory
    {
        public static Content.Content BuildNewContentOriginal(long playerId, long tweetId)
        {
            return new Content.Content
            {
                PlayerId = playerId,
                ContentType = ContentTypes.Original,
                TweetId = tweetId
            };
        }

        public static Content.Content BuildNewContentLike(long playerId, long tweetId)
        {
            return new Content.Content
            {
                PlayerId = playerId,
                ContentType = ContentTypes.Like,
                TweetId = tweetId
            };
        }

        public static Content.Content BuildNewContentReply(ContentCreateDto contentCreateDto, long playerId, long tweetId)
        {
            return new Content.Content
            {
                PlayerId = playerId,
                ContentType = ContentTypes.Reply,
                TweetId = tweetId,
                Message = contentCreateDto.Message,
                ImageUrl = contentCreateDto.ImageUrl
            };
        }

        public static Tweet BuildNewTweet(string message, string imageUrl, long playerId)
        {
            return new Tweet
            {
                Message = message,
                ImageUrl = imageUrl,
                PlayerId = playerId
            };
        }
    }
}