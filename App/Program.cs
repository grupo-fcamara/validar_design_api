using System;
using App.Entities;
using App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App
{
    class Program
    {
        static int Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddLogging(cfg => cfg.AddConsole())
            .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();

            ILogger logger = serviceProvider.GetService<ILogger<Program>>();

            StructuralData data = new StructuralData();
            IGetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            try {
                getEnvironmentVariables.Validate(data);
            } catch(Exception ex) {
                logger.LogInformation(ex.Message);
                return 1;
            }

            ShowData(data);

            return 0;
        }

        public static void ShowData(StructuralData data) 
        {
            var serviceProvider = new ServiceCollection().AddLogging(cfg => cfg.AddConsole())
            .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();
            
            ILogger logger = serviceProvider.GetService<ILogger<Program>>(); 
            
            logger.LogInformation("\nLanguage: " + data.Language + 
            "\nRoutePattern: " + data.RoutePattern +
            "\nVersioned: " + data.Versioned +
            "\n\nHttpVerbs:");
            
            foreach (var value in data.HttpVerbs) {
                logger.LogInformation(value + ", ");
            }

            logger.LogInformation("\n" +
            "PathLevels: " + data.PathLevels +
            "\nStatusCode: {");

            foreach (var value in data.StatusCode) {
                logger.LogInformation("{0}: ", value.Key);

                foreach (var item in value.Value) {
                    
                    logger.LogInformation(item + ", ");
                }

                logger.LogInformation("\n");
            }

            logger.LogInformation("}" + 
            "\nBaseUrl: " + data.BaseUrl +
            "\nSwaggerPath: " + data.SwaggerPath);
        }
    }
}
