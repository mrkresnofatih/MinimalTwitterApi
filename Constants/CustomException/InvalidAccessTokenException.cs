using System;

namespace MinimalTwitterApi.Constants.CustomException
{
    public class InvalidAccessTokenException : Exception
    {
        public InvalidAccessTokenException() : base(ErrorCodes.InvalidAccessToken)
        {
        }
    }
}