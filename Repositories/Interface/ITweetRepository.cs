using System.Threading.Tasks;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface ITweetRepository
    {
        Task<long> AddOne(Tweet tweet);

        Task<bool> LikeExists(long tweetId, long playerId);

        Task<TweetGetDto> GetOne(long tweetId);
    }
}