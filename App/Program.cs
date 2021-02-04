using System;
using System.Linq;
using App.Entities;
using App.Services;
using App.Services.Validations.Level1;
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
            } catch(Exception ex) {
                logger.LogInformation("Error: " + ex.Message);
                return 1;
            }

            ShowData(data);

            var output = new ValidationOutput();
            var documentation = new GetSwaggerService().GetByUrl(data.SwaggerPath);

            //Level 1
            output.Concat(new ValidateIdentifiers().Validate(documentation));
            output.Concat(new ValidatePathLevels(2).Validate(documentation));
            output.Problems.ToList().ForEach(p => logger.LogInformation("Problem: {0}", p));

            return 0;
        }

        static void ConfigureLogging()
        {
            var serviceProvider = new ServiceCollection().AddLogging(cfg => cfg.AddConsole())
            .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();

            logger = serviceProvider.GetService<ILogger<Program>>();
        }

        static void ShowData(StructuralData data) 
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
