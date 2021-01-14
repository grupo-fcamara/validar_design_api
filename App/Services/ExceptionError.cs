using System;

namespace App.Services {
    public class ExceptionError : Exception 
    {
        public string errorMsg;
        public ExceptionError(string msg)
        {
            this.errorMsg = msg;
        }
    }
}

