using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MinimalTwitterApi.Models;

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
            CreateMap<Player, PlayerCreateDto>().ReverseMap();
            CreateMap<Player, PlayerGetDto>().ReverseMap();
        }
    }
}