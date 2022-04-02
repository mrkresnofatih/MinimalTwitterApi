using System;

namespace MinimalTwitterApi.Constants.CustomException
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base(ErrorCodes.RecordNotFound)
        {
        }
    }
}