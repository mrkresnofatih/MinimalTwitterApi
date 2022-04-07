using System;

namespace MinimalTwitterApi.Constants.CustomException
{
    public class AlreadyFollowingException : Exception
    {
        public AlreadyFollowingException() : base(ErrorCodes.AlreadyFollowing)
        {
        }
    }
}