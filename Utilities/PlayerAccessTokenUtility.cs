using System;
using System.Collections.Generic;
using MinimalTwitterApi.Templates;

namespace MinimalTwitterApi.Utilities
{
    public class PlayerAccessTokenUtility : JwtTemplate
    {
        protected override string GetSecret()
        {
            return "8QWmI0Vr0FVXPPCORDObGDsooe7FPvJiXGSzKIdb";
        }

        public string GenerateToken(long playerId)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var payload = new Dictionary<string, string>
            {
                { "playerId", playerId.ToString() },
                { "genesisTime", now.ToString() }
            };
            return GetToken(payload);
        }

        public long GetPlayerIdFromToken(string token)
        {
            var claims = GetPayload(token);
            var playerIdString = claims["playerId"];
            return long.Parse(playerIdString);
        }
    }
}