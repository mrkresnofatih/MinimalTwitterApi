using System;

namespace MinimalTwitterApi.Constants.CustomException
{
    public class UsernameTakenException : Exception
    {
        public UsernameTakenException() : base(ErrorCodes.UsernameTaken)
        {
        }
    }
}