using System.Threading.Tasks;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface ITweetRepository
    {
        Task<long> AddOne(Tweet tweet);
        
        Task<TweetGetDto> GetOne(long tweetId);
    }
}