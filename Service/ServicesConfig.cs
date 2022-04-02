using Microsoft.Extensions.DependencyInjection;

namespace MinimalTwitterApi.Service
{
    public static class ServicesConfig
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
        }
    }
}