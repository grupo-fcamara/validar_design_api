using System;
using System.Linq;
using App.Entities.Environment;
using App.Entities.Swagger;
using App.Entities.Swagger.Two;
using App.Services;
using App.Services.Environment;
using App.Services.Validations;
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
            IDocumentation documentation;

            if (!TryGetData(out data) || !TryGetDocumentation(out documentation, data))
                return 1;

            ShowData(data);
            ValidateApi(documentation, data);

            return 0;
        }

        private static void ValidateApi(IDocumentation documentation, StructuralData data)
        {
            var output = new ApiValidator(data).Validate(documentation);

            foreach (var message in output.Messages)
            {
                if (message.IsProblem)
                    logger.LogInformation("Problem: " + message.Text);
                else
                    logger.LogInformation(message.Text);
            }

            if (output.Value > 0)
                logger.LogInformation($"Your API reached level {output.Value}.");
            else
                logger.LogInformation("Your API hasn't reached any level.");
        }

        static bool TryGetData(out StructuralData data)
        {
            try
            { 
                data = new GetEnvironmentVariables().GetData();
                return true;
            } 
            catch (AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                {
                    logger.LogInformation("Error: " + exception.Message);
                }
                data = null;
                return false;
            }
        }

        static bool TryGetDocumentation(out IDocumentation documentation, StructuralData data)
        {
            try
            {
                documentation = new GetSwaggerService().GetByUrl(data.SwaggerPath);
                return true;
            } 
            catch (Exception e)
            {
                logger.LogInformation("Error: " + e.Message);
                documentation = null;
                return false;
            }   
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
                $"\nPlural: {data.Plural}" +
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
