using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Constants.CustomException;
using MinimalTwitterApi.Repositories;
using MinimalTwitterApi.Utilities;

namespace MinimalTwitterApi.Attributes
{
    public class RequireAuthenticationFilterAttribute : Attribute, IAsyncActionFilter
    {
        public RequireAuthenticationFilterAttribute(PlayerAccessTokenCache playerAccessTokenCache, 
            PlayerAccessTokenUtility playerAccessTokenUtility)
        {
            _playerAccessTokenCache = playerAccessTokenCache;
            _playerAccessTokenUtility = playerAccessTokenUtility;
        }

        private readonly PlayerAccessTokenCache _playerAccessTokenCache;
        private readonly PlayerAccessTokenUtility _playerAccessTokenUtility;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authHeaderExists = context
                .HttpContext
                .Request
                .Headers
                .ContainsKey(CustomHeaders.AuthenticationHeader);
            if (!authHeaderExists)
            {
                throw new InvalidAccessTokenException();
            }

            var token = context.HttpContext.Request.Headers[CustomHeaders.AuthenticationHeader];
            var isTokenValid = _playerAccessTokenUtility.ValidateToken(token);

            if (!isTokenValid)
            {
                throw new InvalidAccessTokenException();
            }

            var playerIdFromToken = _playerAccessTokenUtility.GetPlayerIdFromToken(token);
            var tokenFromRedis = await _playerAccessTokenCache.GetByPlayerId(playerIdFromToken);
            if (tokenFromRedis == null)
            {
                throw new InvalidAccessTokenException();
            }

            var extendTokenLifeTask = _playerAccessTokenCache.SaveByPlayerId(playerIdFromToken, token);
            var controllerExecutionTask = Task.Run(() => next());
            Task.WaitAll(extendTokenLifeTask, controllerExecutionTask);
        }
    }
}