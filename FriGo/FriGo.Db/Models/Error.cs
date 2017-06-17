using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FriGo.Db.Models.Social;

namespace FriGo.Db.Models
{
    public class Error 
    {
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error(HttpStatusCode statusCode, string message)
        {
            Code = (int) statusCode;
            Message = message;
        }

        public Error()
        {
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }
}
