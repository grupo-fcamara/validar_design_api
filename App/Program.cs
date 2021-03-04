using System;
using System.Linq;
using App.Entities.Environment;
using App.Entities.Swagger;
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

            StructuralData data = GetData();
            if (data == null) { return 1; }

            IDocumentation documentation = GetDocumentation(data);
            if (documentation == null) { return 1; }

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

        static StructuralData GetData()
        {
            try { return new GetEnvironmentVariables().GetData(); }
            catch (AggregateException ex)
            {
                foreach (var exception in ex.InnerExceptions)
                    logger.LogInformation("Error: " + exception.Message);
            }
            catch { }
            return null;
        }

        static IDocumentation GetDocumentation(StructuralData data)
        {
            try { return new GetSwaggerService().GetByUrl(data.SwaggerPath); }
            catch (Exception e)
            {
                logger.LogInformation("Error: " + e.Message);
                return null;
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
