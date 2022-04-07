using System;

namespace MinimalTwitterApi.Constants.CustomException
{
    public class AlreadyNotFollowingException : Exception
    {
        public AlreadyNotFollowingException() : base(ErrorCodes.AlreadyNotFollowing)
        {
        }
    }
}