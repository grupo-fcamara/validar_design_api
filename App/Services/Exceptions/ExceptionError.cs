using System;

namespace App.Services.Exceptions {
    public class ExceptionError : Exception 
    {
        public string errorMsg;
        public ExceptionError(string msg)
        {
            this.errorMsg = msg;
        }
    }
}

