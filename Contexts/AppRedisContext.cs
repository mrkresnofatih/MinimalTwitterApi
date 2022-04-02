using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MinimalTwitterApi.Contexts
{
    public static class AppRedisContext
    {
        private static ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            var mtRedisHost = Environment.GetEnvironmentVariable("MT_REDIS_HOST");
            var mtRedisPort = Environment.GetEnvironmentVariable("MT_REDIS_PORT");
            var mtRedisPass = Environment.GetEnvironmentVariable("MT_REDIS_PASS");
            var connectionString = $"{mtRedisHost}:{mtRedisPort},abortConnect=false,password={mtRedisPass}";
            return ConnectionMultiplexer.Connect(connectionString);
        }

        public static void AddAppRedisService(this IServiceCollection services)
        {
            var redis = CreateConnectionMultiplexer();
            services.AddSingleton(redis.GetDatabase());
        }
    }
}