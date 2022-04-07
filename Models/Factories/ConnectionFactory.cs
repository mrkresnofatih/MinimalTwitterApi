using MinimalTwitterApi.Constants;

namespace MinimalTwitterApi.Models.Factories
{
    public static class ConnectionFactory
    {
        public static Connection BuildNewConnectionFollowing(long targetId, long playerId)
        {
            return new Connection
            {
                ConnectionType = ConnectionTypes.Following,
                PlayerId = targetId,
                FollowerId = playerId
            };
        }

        public static Connection BuildNewConnectionNotFollowing(long targetId, long playerId)
        {
            return new Connection
            {
                ConnectionType = ConnectionTypes.NotFollowing,
                PlayerId = targetId,
                FollowerId = playerId
            };
        }
    }
}