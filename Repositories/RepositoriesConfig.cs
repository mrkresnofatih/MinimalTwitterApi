using Microsoft.Extensions.DependencyInjection;

namespace MinimalTwitterApi.Repositories
{
    public static class RepositoriesConfig
    {
        public static void AddAppRepositories(this IServiceCollection services)
        {
            services.AddScoped<PlayerRepository>();
            services.AddSingleton<PlayerAccessTokenCache>();
        }
    }
}