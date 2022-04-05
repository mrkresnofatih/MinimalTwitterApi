using System.Threading.Tasks;
using MinimalTwitterApi.Models.Content;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Controllers.Interfaces
{
    public interface IContentController
    {
        Task<ResponsePayload<ContentGetDto>> AddOneOriginal(ContentCreateDto contentCreateDto, string token);

        Task<ResponsePayload<ContentGetDto>> AddOneLike(long tweetId, string token);
        
        Task<ResponsePayload<ContentGetDto>> AddOneReply(ContentCreateDto contentCreateDto, long tweetId, string token);

        Task<ResponsePayload<ContentGetDto>> GetOne(long contentId);
    }
}