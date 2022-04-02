using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using MinimalTwitterApi.Constants;
using MinimalTwitterApi.Constants.CustomException;

namespace MinimalTwitterApi.Utilities
{
    public static class ExceptionHandler
    {
        public static void UseAppExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exception = context
                        .Features
                        .Get<IExceptionHandlerPathFeature>()
                        .Error;
                    var errorCode = GetErrorCode(exception);
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsJsonAsync(ResponseHandler.WrapFailure<object>(errorCode));
                });
            });
        }

        private static string GetErrorCode(Exception exception)
        {
            switch (exception)
            {
                case UsernameTakenException:
                    return ErrorCodes.UsernameTaken;
                case InvalidAccessTokenException:
                case IncorrectPasswordException:
                    return ErrorCodes.InvalidCredentials;
                case RecordNotFoundException:
                    return ErrorCodes.BadRequest;
                default:
                    return ErrorCodes.Unhandled;
            }
        }
    }
}