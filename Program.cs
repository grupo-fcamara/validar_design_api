using System;

namespace validar_design_api
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpVerbs httpVerbs = new HttpVerbs();
            System.Console.WriteLine("httpVerbs.Value[0]: ", httpVerbs.Value[0]);
            LanguageVariable language = new LanguageVariable();
            System.Console.WriteLine("Language: " + language.Value);
            PathLevels pathLevels = new PathLevels();
            System.Console.WriteLine("PathLevels: " + pathLevels.Value);
            RoutePattern routePattern = new RoutePattern();
            System.Console.WriteLine("RoutePattern: " + routePattern.Value);
            StatusCodeVariable statusCode = new StatusCodeVariable();
            System.Console.WriteLine("statusCode.Value.GET[0]: " + statusCode.Value.GET[0]);
            Versioned versioned = new Versioned();
            System.Console.WriteLine("Versioned: " + versioned.Value);

            BaseUrl baseUrl = new BaseUrl();
            System.Console.WriteLine("BaseUrl: "+ baseUrl.Value);
            SwaggerPath swaggerPath = new SwaggerPath();
            System.Console.WriteLine("SwaggerPath: " + swaggerPath.Value);
        }
    }
}
