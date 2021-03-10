using GameSource.Models.Enums;

namespace GameSource.Models
{
    public class ApiResponse
    {
        public ResponseStatusCode ResponseStatusCode { get; set; }
        public string Message { get; set; } = "";
        public int NumberOfRows { get; set; } = 0;
        public object Data { get; set; }

        public ApiResponse(object data, ResponseStatusCode responseStatusCode, string message, int numberOfRows = 0)
        {
            ResponseStatusCode = responseStatusCode;
            Message = message ?? GetDefaultMessageForStatusCode(responseStatusCode);
            Data = data;
            NumberOfRows = numberOfRows != 0 ? numberOfRows : 0;
        }

        public ApiResponse(ResponseStatusCode responseStatusCode, string message, int numberOfRows = 0)
        {
            ResponseStatusCode = ResponseStatusCode;
            Message = message ?? GetDefaultMessageForStatusCode(responseStatusCode);
            NumberOfRows = numberOfRows != 0 ? numberOfRows : 0;
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
