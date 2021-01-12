using System;

namespace validar_design_api
{
    public class BaseUrl
    {
        public string Value { get; private set; }
        
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