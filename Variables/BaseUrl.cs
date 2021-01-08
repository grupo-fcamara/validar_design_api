using System;

namespace validar_design_api
{
    class BaseUrl
    {
        public string Value { get; set; }
        
        public BaseUrl()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("BASE_URL");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            Value = value;
        }
    }
}