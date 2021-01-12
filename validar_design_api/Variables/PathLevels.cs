using System;

namespace validar_design_api
{
    public class PathLevels
    {
        public int Value { get; private set; }

        public PathLevels()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("NIVEIS_PATH");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            Value = int.Parse(value);
        }
    }
}