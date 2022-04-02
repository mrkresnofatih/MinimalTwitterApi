using Microsoft.Extensions.DependencyInjection;

namespace MinimalTwitterApi.Contexts
{
    public static class AppDbConfig
    {
        public static void AddAppDbContexts(this IServiceCollection services)
        {
            services.AddMtDbContext();
            services.AddAppRedisService();
        }
    }
}