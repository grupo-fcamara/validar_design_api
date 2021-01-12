using System;

namespace validar_design_api
{
    public enum Case { PLURAL, SINGULAR, CAMEL, SNAKE, SPINAL }
    
    public class RoutePattern
    {
        public Case Value { get; private set; }

        public RoutePattern()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("PADRAO_ROTAS");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            switch (value) {
                case "PLURAL":
                    Value = Case.PLURAL;
                    break;
                
                case "SINGULAR":
                    Value = Case.SINGULAR;
                    break;

                case "CAMEL":
                    Value = Case.CAMEL;
                    break;

                case "SNAKE":
                    Value = Case.SNAKE;
                    break;
                
                case "SPINAL":
                    Value = Case.SPINAL;
                    break;
                
                default:
                    // tratar erro
                    Environment.Exit(1);
                    break;
            }

        }
    }
}