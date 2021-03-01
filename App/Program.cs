using System;
using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using App.Entities.Swagger.Two;
using App.Services;
using App.Services.Validations;
using App.Services.Validations.Generic;
using App.Services.Validations.Level1;
using App.Services.Validations.Level2;
using App.Services.Validations.Level3;
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

            StructuralData data;

            try { data = new GetEnvironmentVariables().GetData(); } 
            catch (AggregateException ex) {
                foreach (var exception in ex.InnerExceptions)
                {
                    logger.LogInformation("Error: " + exception.Message);
                }
                return 1;
            }

            ShowData(data);

            IDocumentation documentation = new Documentation();

            try {
                documentation = new GetSwaggerService().GetByUrl(data.SwaggerPath);
            } catch (Exception e) {
                logger.LogInformation("Error: " + e.Message);
                return 1;
            }        

            var output = ValidateApi(documentation, data);

            foreach (var problem in output.Problems)
            {
                logger.LogInformation("Problem: " + problem);
            }

            if (output.Value > 0)
                logger.LogInformation($"Your API reached level {output.Value}.");
            else
                logger.LogInformation("Your API hasn't reached any level.");

            return 0;
        }

        private static ValidationOutput<int> ValidateApi(IDocumentation documentation, StructuralData data)
        {
            ILevel[] levels = new ILevel[]
            {
                new ValidationLevel(1,
                    new ValidatePathLevels(data.PathLevels),
                    new ValidateGetRoutesPerPath(),
                    new ValidateIdentifiers()
                ),

                new ValidationLevel(2,
                    new ValidateRoutesPattern(data.RoutePattern, data.Plural),
                    new ValidatePathOperations()
                ),

                new ValidationLevel(3,
                    new ValidatePathHttpVerbs(data.HttpVerbs),
                    new ValidateStatusCode(data.StatusCode)
                )
            };

            var output = new ValidationOutput<int>();
            int apiLevel = levels.Max(l => l.Level);

            foreach (var level in levels.OrderBy(l => l.Level))
            {
                var levelOutput = level.Validate(documentation);
                if (!levelOutput.All(o => o.Ok))
                    apiLevel = level.Level - 1;

                output.Concat(levelOutput);
            }

            output.Value = apiLevel;
            return output;
        }

        static void ConfigureLogging()
        {
            var serviceProvider = new ServiceCollection().AddLogging(cfg => cfg.AddConsole())
            .Configure<LoggerFilterOptions>(cfg => cfg.MinLevel = LogLevel.Debug).BuildServiceProvider();

            logger = serviceProvider.GetService<ILogger<Program>>();
        }

        static void ShowData(StructuralData data) 
        {
            string statusCode = "{\n";
            foreach (var pair in data.StatusCode.Where(pair => pair.Value.Any())) {
                statusCode += $"\t{pair.Key}: [{string.Join(", ", pair.Value)}]\n";
            }
            statusCode += "}";

            logger.LogInformation(
                $"\nLanguage: {data.Language}" + 
                $"\nRoutePattern: {data.RoutePattern}" +
                $"\nPlural: {data.Plural}" + "\n" +
                $"\nVersioned: {data.Versioned}" + "\n" +
                $"\nHttpVerbs: {string.Join(", ", data.HttpVerbs)}" +
                $"\nPathLevels: {data.PathLevels}" +
                $"\nStatusCode: {statusCode}" + "\n" +
                $"\nBaseUrl: {data.BaseUrl}"+
                $"\nSwaggerPath: {data.SwaggerPath}"
            );
        }
    }
}
