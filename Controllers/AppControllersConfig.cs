using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Controllers
{
    public static class AppControllersConfig
    {
        public static void AddAppControllers(this IServiceCollection services)
        {
            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = context.HttpContext.RequestServices
                        .GetRequiredService<ILogger<Startup>>();
                    logger.LogInformation("Invalid ModelState");
                    return new OkObjectResult(ResponseHandler.WrapFailure<object>(ErrorCodes.BadRequest));
                };
            });
        }
    }
}