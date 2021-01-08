using System;

namespace validar_design_api
{
    class Versioned : IVariableValidation
    {
        public string Value { get; set; }

        public Versioned()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("PATH_VERSIONADO");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            Value = value;
        }
    }
}