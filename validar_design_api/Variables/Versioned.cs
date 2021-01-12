using System;

namespace validar_design_api
{
    public class Versioned : IVariableValidation
    {
        public bool Value { get; private set; }

        public Versioned()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("PATH_VERSIONADO");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            Value = bool.Parse(value);
        }
    }
}