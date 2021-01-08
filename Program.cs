using System;

namespace validar_design_api
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpVerbs httpVerbs = new HttpVerbs();
            LanguageVariable language = new LanguageVariable();
            System.Console.WriteLine(language.Value);
            PathLevels pathLevels = new PathLevels();
            RoutePattern routePattern = new RoutePattern();
            StatusCodeVariable statusCode = new StatusCodeVariable();
            Versioned versioned = new Versioned();

            BaseUrl baseUrl = new BaseUrl();
            SwaggerPath swaggerPath = new SwaggerPath();
        }
    }
}
