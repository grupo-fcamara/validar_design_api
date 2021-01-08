using System;

namespace validar_design_api
{
    class SwaggerPath
    {
        public string Value { get; set; }

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