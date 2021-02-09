using System;
using App.Entities;
using App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App
{
    class Program
    {
        static ILogger logger;

        static int Main(string[] args)
        {
            ConfigureLogging();
            logger.LogInformation("Executing...");

            StructuralData data = new StructuralData();
            IGetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            try {
                getEnvironmentVariables.Validate(data);
            } catch (AggregateException ex) {
                foreach (var exception in ex.InnerExceptions)
                {
                    logger.LogInformation("Error: " + exception.Message);
                }
                return 1;
            }

            ShowData(data);

            return 0;
        }

        static void ConfigureLogging()
        {
            var serviceProvider = new ServiceCollection().AddLogging(cfg => cfg.AddConsole())
            .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();

            logger = serviceProvider.GetService<ILogger<Program>>();
        }

        static void ShowData(StructuralData data) 
        {
            string httpVerbs = "";
            for (int i = 0; i < data.HttpVerbs.Length; i++) {
                httpVerbs += data.HttpVerbs[i];
                if (i < data.HttpVerbs.Length - 1)
                    httpVerbs += ", ";
            }

            string statusCode = "{\n";
            foreach (var pair in data.StatusCode) {
                statusCode += "\t" + pair.Key + ": ";

                for (int i = 0; i < pair.Value.Length; i++) {
                    statusCode += pair.Value[i];
                    if (i < pair.Value.Length - 1)
                        statusCode += ", ";
                }

                statusCode += "\n";
            }
            statusCode += "}";

            logger.LogInformation(
                $"\nLanguage: {data.Language}" + 
                $"\nRoutePattern: {data.RoutePattern}" +
                $"\nVersioned: {data.Versioned}" + "\n" +
                $"\nHttpVerbs: {httpVerbs}" +
                $"\nPathLevels: {data.PathLevels}" +
                $"\nStatusCode: {statusCode}" + "\n" +
                $"\nBaseUrl: {data.BaseUrl}"+
                $"\nSwaggerPath: {data.SwaggerPath}"
            );
        }
    }
}
