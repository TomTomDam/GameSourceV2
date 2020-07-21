using GameSource.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Models
{
    public class ApiResponse
    {
        public ResponseStatusCode ResponseStatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(ResponseStatusCode responseStatusCode, string message = null)
        {
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
