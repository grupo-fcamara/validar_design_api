using System;

namespace validar_design_api
{
    public class SwaggerPath
    {
        public string Value { get; private set; }

        public SwaggerPath()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("PATH_SWAGGER");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            Value = value;
        }
    }
}