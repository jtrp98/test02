using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Models
{
    public class BaseResultModel
    {
        public string Message { get; set; }

        internal Exception SystemErrorMessage { get; set; }
        public string MessageError
        {
            get
            {
                if (SystemErrorMessage == null)
                {
                    return string.Empty;
                }
                else
                {
                    return "{" + string.Format("\"Message\": \"{0}\" ,\"StackTrace\":\"{1}\",\"Source\":\"{2}\" ",
       SystemErrorMessage.Message, SystemErrorMessage.StackTrace, SystemErrorMessage.Source) + "}";
                }
            }
        }

        public string StatusCode { get; set; }

        public object Result { get; set; }

        public int ResultCount { get; set; }

        public string Title { get; set; }
    }
}