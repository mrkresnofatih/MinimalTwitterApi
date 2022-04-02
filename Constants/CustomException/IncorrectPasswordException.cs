using System;

namespace MinimalTwitterApi.Constants.CustomException
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException() : base(ErrorCodes.InvalidCredentials)
        {
        }
    }
}