using System;

namespace MinimalTwitterApi.Constants.CustomException
{
    public class TweetAlreadyLikedException : Exception
    {
        public TweetAlreadyLikedException() : base(ErrorCodes.TweetAlreadyLikes)
        {
        }
    }
}