using System;

namespace Api.Base.ExceptionFilter
{
    public abstract class HttpCodeException : Exception
    {
        public ApiErrorResponse Response { get; set; }

        public HttpCodeException(string message) : base(message)
        {
            Response = new ApiErrorResponse
            {
                ErrorCode = GetStatusCode(),
                ErrorMesage = $"{GetErrorPrefix()}: {message}"
            };
        }

        protected abstract int GetStatusCode();

        protected abstract string GetErrorPrefix();
    }
}
