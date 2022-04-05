using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinimalTwitterApi.Models;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Contexts
{
    public class MtDbContext : DbContext
    {
        public MtDbContext(DbContextOptions<MtDbContext> options) : base(options)
        {
        }
        
        public DbSet<Player> Players { get; set; }
        
        public DbSet<Content> Contents { get; set; }
        
        public DbSet<Tweet> Tweets { get; set; }
    }

    public static class MtDbConfig
    {
        public static void AddMtDbContext(this IServiceCollection services)
        {
            var mtDbHost = Environment.GetEnvironmentVariable("MT_DB_HOST");
            var mtDbPort = Environment.GetEnvironmentVariable("MT_DB_PORT");
            var mtDbUsername = Environment.GetEnvironmentVariable("MT_DB_USERNAME");
            var mtDbPassword = Environment.GetEnvironmentVariable("MT_DB_PASSWORD");
            var connectionString =
                $@"Host={mtDbHost};Port={mtDbPort};Username={mtDbUsername};Password={mtDbPassword};Database=mtDb";
            services.AddDbContext<MtDbContext>(opt => opt.UseNpgsql(connectionString));
        }
    }
}