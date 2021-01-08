using System;
using System.Text.Json;

namespace validar_design_api
{
    class StatusCode
    {
        public int[] GET { get; set; }
        public int[] POST { get; set; }
        public int[] PUT { get; set; }
        public int[] DELETE { get; set; }
    }

    class StatusCodeVariable : IVariableValidation
    {
        public StatusCode Value { get; set; }

        public StatusCodeVariable()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("STATUS_CODE");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            StatusCode statusCode = JsonSerializer.Deserialize<StatusCode>(value);
            Value = statusCode;
        }
    }
}