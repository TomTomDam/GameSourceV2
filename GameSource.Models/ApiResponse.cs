using GameSource.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSource.Models
{
    public class ApiResponse
    {
        public ResponseStatusCode ResponseStatusCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
