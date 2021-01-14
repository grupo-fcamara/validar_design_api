using System;

namespace validar_design_api.Services {
    public class ExceptionError : Exception 
    {
        public string errorMsg;
        public ExceptionError(string msg)
        {
            this.errorMsg = msg;
        }
    }
}

