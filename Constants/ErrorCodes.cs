namespace MinimalTwitterApi.Constants
{
    public static class ErrorCodes
    {
        public const string BadRequest = "BAD_REQUEST";
        public const string RecordNotFound = "RECORD_NOT_FOUND";
        public const string InvalidCredentials = "INVALID_CREDENTIALS";
        public const string InvalidAccessToken = "INVALID_ACCESS_TOKEN";
        public const string UsernameTaken = "USERNAME_TAKEN";
        public const string TweetAlreadyLikes = "TWEET_ALREADY_LIKED";
        public const string AlreadyFollowing = "ALERADY_FOLLOWING";
        public const string AlreadyNotFollowing = "ALERADY_NOT_FOLLOWING";
        public const string Unhandled = "UNHANDLED";
    }
}