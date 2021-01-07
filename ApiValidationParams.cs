using System;

namespace validar_design_api
{
    public enum Language { ENGLISH, PORTUGUESE }
    public enum Case { PLURAL, SINGULAR, CAMEL, SNAKE, SPINAL }
    public enum HttpVerbs { GET = 1, POST = 2, PUT = 4, DELETE = 8, HEAD = 16, PATCH = 32, OPTIONS = 64 }

    class ApiValidationParams
    {
        public Language Language { get; set; }
        public Case RoutePattern { get; set; }
        public bool Versioned { get; set; }
        public HttpVerbs[] HttpVerbs { get; set; } //sugerir padrão
        private StatusCode AllowedStatusCodeByVerb; //sugerir padrão
        public int PathLevels { get; set; }
        public string BaseUrl { get; set; }
        public string SwaggerPath { get; set; }

        public ApiValidationParams()
        {
            Language = EnvironmentVariables.GetLanguage();
            System.Console.WriteLine(Language);

            RoutePattern = EnvironmentVariables.GetRoutePattern();
            System.Console.WriteLine(RoutePattern);

            Versioned = EnvironmentVariables.GetVersioned();
            System.Console.WriteLine(Versioned);
            
            HttpVerbs = EnvironmentVariables.GetHttpVerbs();
            foreach (var item in HttpVerbs)
            {
                System.Console.WriteLine(item);
            }

            AllowedStatusCodeByVerb = new StatusCode();
            AllowedStatusCodeByVerb = EnvironmentVariables.GetStatusCode();
            System.Console.WriteLine(AllowedStatusCodeByVerb.POST[1]);

            PathLevels = EnvironmentVariables.GetPathLevels();
            System.Console.WriteLine(PathLevels);

            BaseUrl = EnvironmentVariables.GetBaseURL();
            System.Console.WriteLine(BaseUrl);

            SwaggerPath = EnvironmentVariables.GetSwaggerPath();
            System.Console.WriteLine(SwaggerPath);
        }

        /*public ApiValidationParams(Language language, Case casePattern, bool isVersioned, HttpVerbs allowedVerbs, int levelsAllowed)
        {
            Language = language;
            CasePattern = casePattern;
            Versioned = isVersioned;
            HttpVerbs = allowedVerbs;
            PathLevels = PathLevels;
        }*/
    }
}