using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class BaseResponseObject
    {
        public HttpStatusCode ResponseStatusCode { get; set; }
        public string ErrorResponse { get; set; }
    }
}
