namespace Api.Base.Constants
{
    public class ErrorConstants
    {
        // Error Response Code
        public static readonly int DataInvalidCode = 400;
        public static readonly int UnAuthorizedCode = 401;
        public static readonly int ForbidenCode = 403;
        public static readonly int NotFoundCode = 404;
        public static readonly int MethodNotAlowedCode = 405;

        // Error Response Message
        public static readonly string DataInvalidMessage = "Yêu cầu không hợp lệ";
        public static readonly string UnAuthorizedMessage = "Yêu cầu xác thực";
        public static readonly string ForbidenMessage = "Truy cập bị cấm";
        public static readonly string NotFoundMessage = "Không tìm thấy tài nguyên";
        public static readonly string MethodNotAlowedMessage = "Hành động không được phép";

        // Model State Invalid Message
        public static readonly string CompareError = "{0} không được dài hơn {1} ký tự";
        public static readonly string CreditCardError = "{0} không được dài hơn {1} ký tự";
        public static readonly string DataTypeError = "{0} không thuộc kiểu dữ liệu {1}";
        public static readonly string EnumDataTypeError = "{0} không thuộc kiểu dữ liệu {1}";
        public static readonly string EmailAddressError = "{0} : Email không hợp lệ";
        public static readonly string FileExtensionsAddressError = "{0} không thuộc kiểu tệp tin {1}";
        public static readonly string MaxLengthError = "{0} không được dài hơn {1} ký tự";
        public static readonly string MinLengthError = "{0} không được ngắn hơn {1} ký tự";
        public static readonly string PhoneError = "{0} : SĐT không hợp lệ";
        public static readonly string StringLengthError = "{0} phải có độ dài trong khoảng {1} và {2} ký tự";
        public static readonly string RangeError = "{0} không được nằm ngoài phạm vi [{1} - {2}]";
        public static readonly string RegularExpressionError = "{0} không phù hợp với biểu thức";
        public static readonly string RequiredError = "{0} không được để trống";
        public static readonly string UrlError = "{0} : URL không hợp lệ";
    }
}
