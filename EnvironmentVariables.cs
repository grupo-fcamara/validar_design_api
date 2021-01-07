using System;
using System.Text.Json;

namespace validar_design_api {
    class EnvironmentVariables
    {
        public static Language GetLanguage() {
            string value = Environment.GetEnvironmentVariable("IDIOMA");
            switch (value) {
                case "ENGLISH":
                    return Language.ENGLISH;
                    
                case "PORTUGUESE":
                    return Language.PORTUGUESE;

                default:
                    return 0;
            }
        }

        public static Case GetRoutePattern() {
            string value = Environment.GetEnvironmentVariable("PADRAO_ROTAS");
            switch (value) {
                case "PLURAL":
                    return Case.PLURAL;
                
                case "SINGULAR":
                    return Case.SINGULAR;

                case "CAMEL":
                    return Case.CAMEL;

                case "SNAKE":
                    return Case.SNAKE;
                
                case "SPINAL":
                    return Case.SPINAL;
                
                default:
                    return 0;
            }
        }

        public static bool GetVersioned() {
            string value = Environment.GetEnvironmentVariable("PATH_VERSIONADO");
            return bool.Parse(value);
        }

        public static HttpVerbs[] GetHttpVerbs() {
            string value = Environment.GetEnvironmentVariable("VERBOS_HTTP");
            string[] verbosHttp = JsonSerializer.Deserialize<string[]>(value);
            HttpVerbs[] result = new HttpVerbs[verbosHttp.Length];

            for (int i = 0; i < verbosHttp.Length; i++) {
                switch (verbosHttp[i]) {
                    case "GET":
                        result[i] = HttpVerbs.GET;
                        break;

                    case "POST":
                        result[i] = HttpVerbs.POST;
                        break;

                    case "PUT":
                        result[i] = HttpVerbs.PUT;
                        break;
                    
                    case "DELETE":
                        result[i] = HttpVerbs.DELETE;
                        break;
                    
                    case "HEAD":
                        result[i] = HttpVerbs.HEAD;
                        break;

                    case "PATCH":
                        result[i] = HttpVerbs.PATCH;
                        break;

                    case "OPTIONS":
                        result[i] = HttpVerbs.OPTIONS;
                        break;

                    default:
                        System.Console.WriteLine(verbosHttp[i] + " isn't a HTTP Verb\n" +
                        "Existing HTTP Verbs: 'GET', 'POST', 'PUT', 'DELETE', 'HEAD', 'PATCH' and 'OPTIONS'. ");
                        Environment.Exit(1);
                        break;
                }
            }
            return result;
        }

        public static StatusCode GetStatusCode() {
            string value = Environment.GetEnvironmentVariable("STATUS_CODE");
            StatusCode statusCode = JsonSerializer.Deserialize<StatusCode>(value);
            return statusCode;
        }

        public static int GetPathLevels() {
            string value = Environment.GetEnvironmentVariable("NIVEIS_PATH");
            return int.Parse(value);
        }

        public static string GetBaseURL() {
            string value = Environment.GetEnvironmentVariable("BASE_URL");
            return value;
        }

        public static string GetSwaggerPath() {
            string value = Environment.GetEnvironmentVariable("PATH_SWAGGER");
            return value;
        }
    }
}