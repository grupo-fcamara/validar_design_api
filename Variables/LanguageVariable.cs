using System;

namespace validar_design_api
{
    public enum Language { ENGLISH, PORTUGUESE }
    class LanguageVariable
    {
        public Language Value { get; set; }

        public LanguageVariable()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("IDIOMA");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            switch (value) {
                case "ENGLISH":
                    Value = Language.ENGLISH;
                    break;

                case "PORTUGUESE":
                    Value = Language.PORTUGUESE;
                    break;

                default:
                    // tratar erro
                    Environment.Exit(1);
                    break;
            }
        }
    }
}