using System;

namespace validar_design_api
{
    class PathLevels
    {
        public string Value { get; set; }

        public PathLevels()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("NIVEIS_PATH");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            Value = value;
        }
    }
}