using Api.Base.Constants;

namespace Api.Base.ExceptionFilter.Exceptions
{
    public class MethodNotAlowedException : HttpCodeException
    {
        public MethodNotAlowedException(string message) : base(message)
        {
        }

        protected override string GetErrorPrefix()
        {
            return ErrorConstants.MethodNotAlowedMessage;
        }

        protected override int GetStatusCode()
        {
            return ErrorConstants.MethodNotAlowedCode;
        }
    }
}
