using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MinimalTwitterApi.Attributes;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Controllers.Interfaces;
using MinimalTwitterApi.Models.Content;
using MinimalTwitterApi.Service;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentController : ControllerBase, IContentController
    {
        public ContentController(ContentService contentService)
        {
            _contentService = contentService;
        }
        private readonly ContentService _contentService;

        [HttpPost("addOriginal")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<ContentGetDto>> AddOneOriginal([FromBody] ContentCreateDto contentCreateDto,
            [FromHeader(Name = CustomHeaders.AuthenticationHeader)] string token)
        {
            var res = await _contentService.AddOriginal(contentCreateDto, token);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("addLike/{tweetId}")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<ContentGetDto>> AddOneLike(long tweetId, 
            [FromHeader(Name = CustomHeaders.AuthenticationHeader)] string token)
        {
            var res = await _contentService.AddLike(tweetId, token);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpPost("addReply/{tweetId}")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<ContentGetDto>> AddOneReply(ContentCreateDto contentCreateDto, 
            long tweetId, [FromHeader(Name = CustomHeaders.AuthenticationHeader)] string token)
        {
            var res = await _contentService.AddReply(contentCreateDto, tweetId, token);
            return ResponseHandler.WrapSuccess(res);
        }

        [HttpGet("{contentId}")]
        [TypeFilter(typeof(RequireAuthenticationFilterAttribute))]
        public async Task<ResponsePayload<ContentGetDto>> GetOne(long contentId)
        {
            var res = await _contentService.GetOne(contentId);
            return ResponseHandler.WrapSuccess(res);
        }
    }
}