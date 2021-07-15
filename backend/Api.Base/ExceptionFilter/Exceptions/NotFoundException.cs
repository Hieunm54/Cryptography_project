using Api.Base.Constants;

namespace Api.Base.ExceptionFilter.Exceptions
{
    public class NotFoundException : HttpCodeException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        protected override string GetErrorPrefix()
        {
            return ErrorConstants.NotFoundMessage;
        }

        protected override int GetStatusCode()
        {
            return ErrorConstants.NotFoundCode;
        }
    }
}
