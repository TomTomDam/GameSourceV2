using GameSource.Models.Enums;

namespace GameSource.Models
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public ResponseStatusCode ResponseStatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(object data, ResponseStatusCode responseStatusCode, string message = null)
        {
            Data = data;
            ResponseStatusCode = responseStatusCode;
            Message = message ?? GetDefaultMessageForStatusCode(responseStatusCode);
        }

        public static string GetDefaultMessageForStatusCode(ResponseStatusCode responseStatusCode)
        {
            switch (responseStatusCode)
            {
                case ResponseStatusCode.Success:
                    return "";
                case ResponseStatusCode.Error:
                    return "An unexpected error has occurred. Please contact your administrator.";
                default:
                    return null;
            }
        }
    }
}
