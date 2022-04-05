using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Utilities
{
    public static class AutomapperUtility
    {
        private static IMapper CreateIMapper()
        {
            var mapperProfile = new MapperConfiguration(mp => mp.AddProfile(new AutomapperProfile()));
            return mapperProfile.CreateMapper();
        }

        public static void AddAutomapperConfig(this IServiceCollection services)
        {
            services.AddSingleton(CreateIMapper());
        }
    }

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // Player
            CreateMap<Player, PlayerCreateDto>().ReverseMap();
            CreateMap<Player, PlayerGetDto>().ReverseMap();
            
            // Content
            CreateMap<Content, ContentCreateDto>().ReverseMap();
            CreateMap<Tweet, ContentCreateDto>().ReverseMap();
            CreateMap<Tweet, TweetGetDto>().ReverseMap();
        }
    }
}