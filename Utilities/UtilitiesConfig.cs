using Microsoft.Extensions.DependencyInjection;

namespace MinimalTwitterApi.Utilities
{
    public static class UtilitiesConfig
    {
        public static void AddAppUtilities(this IServiceCollection services)
        {
            services.AddAutomapperConfig();
            services.AddSingleton<PlayerPasswordUtility>();
            services.AddSingleton<PlayerAccessTokenUtility>();
        }
    }
}