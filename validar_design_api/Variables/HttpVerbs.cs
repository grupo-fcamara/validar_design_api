using System;
using System.Text.Json;

namespace validar_design_api
{
    public enum EHttpVerbs { GET = 1, POST = 2, PUT = 4, DELETE = 8, HEAD = 16, PATCH = 32, OPTIONS = 64 }

    public class HttpVerbs : IVariableValidation
    {
        public EHttpVerbs[] Value { get; private set; }

        public HttpVerbs()
        {
            GetEnvironmentVariable();
        }
        public void GetEnvironmentVariable() {
            string value = Environment.GetEnvironmentVariable("VERBOS_HTTP");
            ValidateVariable(value);
        }
        public void ValidateVariable(string value) {
            string[] verbosHttp = JsonSerializer.Deserialize<string[]>(value);
            Value = new EHttpVerbs[verbosHttp.Length];

            for (int i = 0; i < verbosHttp.Length; i++) {
                switch (verbosHttp[i]) {
                    case "GET":
                        Value[i] = EHttpVerbs.GET;
                        break;

                    case "POST":
                        Value[i] = EHttpVerbs.POST;
                        break;

                    case "PUT":
                        Value[i] = EHttpVerbs.PUT;
                        break;
                    
                    case "DELETE":
                        Value[i] = EHttpVerbs.DELETE;
                        break;
                    
                    case "HEAD":
                        Value[i] = EHttpVerbs.HEAD;
                        break;

                    case "PATCH":
                        Value[i] = EHttpVerbs.PATCH;
                        break;

                    case "OPTIONS":
                        Value[i] = EHttpVerbs.OPTIONS;
                        break;

                    default:
                        System.Console.WriteLine(verbosHttp[i] + " isn't a HTTP Verb\n" +
                        "Existing HTTP Verbs: 'GET', 'POST', 'PUT', 'DELETE', 'HEAD', 'PATCH' and 'OPTIONS'. ");
                        Environment.Exit(1);
                        break;
                }
            }
        }

        
    }
}