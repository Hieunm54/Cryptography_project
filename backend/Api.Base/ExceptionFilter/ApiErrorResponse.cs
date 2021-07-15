namespace Api.Base.ExceptionFilter
{
    public class ApiErrorResponse
    {
        public int ErrorCode { get; set; }
        public string ErrorMesage { get; set; }
        public string RequestId { get; set; }

        public ApiErrorResponse()
        {
        }

        public ApiErrorResponse(int errorCode, string errorMesage, string requestId)
        {
            ErrorCode = errorCode;
            ErrorMesage = errorMesage;
            RequestId = requestId;
        }
    }
}
