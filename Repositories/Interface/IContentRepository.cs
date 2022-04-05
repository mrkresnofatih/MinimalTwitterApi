using System.Threading.Tasks;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Repositories.Interface
{
    public interface IContentRepository
    {
        Task<long> AddOne(Content content);

        Task<ContentGetDto> GetOne(long contentId);
    }
}