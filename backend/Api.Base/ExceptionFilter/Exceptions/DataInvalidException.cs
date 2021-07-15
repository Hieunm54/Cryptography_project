using Api.Base.Constants;

namespace Api.Base.ExceptionFilter.Exceptions
{
    public class DataInvalidException : HttpCodeException
    {
        public DataInvalidException(string message) : base(message)
        {
        }

        protected override string GetErrorPrefix()
        {
            return ErrorConstants.DataInvalidMessage;
        }

        protected override int GetStatusCode()
        {
            return ErrorConstants.DataInvalidCode;
        }
    }
}
