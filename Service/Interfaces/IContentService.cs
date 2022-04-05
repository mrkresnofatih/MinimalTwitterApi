using System.Threading.Tasks;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Service.Interfaces
{
    public interface IContentService
    {
        Task<ContentGetDto> AddOriginal(ContentCreateDto contentCreateDto, string token);

        Task<ContentGetDto> AddLike(long tweetId, string token);

        Task<ContentGetDto> AddReply(ContentCreateDto contentCreateDto, long tweetId, string token);

        Task<ContentGetDto> GetOne(long contentId);
    }
}