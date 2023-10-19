using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchExample.Application
{
    public class ResponseHandler
    {
        public ResponseHandler(System.Net.HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Success = statusCode == System.Net.HttpStatusCode.OK;
        }

        public ResponseHandler(bool success, IList<string> messages, System.Net.HttpStatusCode? statusCode = null)
        {
            Success = success;
            Messages = messages;
            StatusCode = statusCode ?? System.Net.HttpStatusCode.BadRequest;
        }

        public ResponseHandler(bool success, string message, System.Net.HttpStatusCode? statusCode = null)
        {
            Success = success;
            StatusCode = statusCode ?? System.Net.HttpStatusCode.InternalServerError;
            Messages = new List<string>
            {
                message
            };
        }

        public ResponseHandler(dynamic data, System.Net.HttpStatusCode? statusCode = null)
        {
            Success = true;
            StatusCode = statusCode ?? System.Net.HttpStatusCode.OK;
            Data = data;
        }

        public IList<string> Messages { get; }
        public bool Success { get; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public dynamic Data { get; }
    }
}
