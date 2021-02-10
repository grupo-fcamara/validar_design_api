using System;
using System.Text.Json;
using System.Collections.Generic;
using App.Entities;

namespace App.Services
{
    public class GetEnvironmentVariables : IGetEnvironmentVariables
    {
        public bool Validate(StructuralData data) {

            var exceptions = new List<Exception>();
            
            if ((int) (data.Language = GetLanguage()) <= 0)
                exceptions.Add(new Exception($"LANGUAGE variable not set properly, available languages: {LANG.ENGLISH}, {LANG.PORTUGUESE}"));

            if ((int) (data.RoutePattern = GetRoutePattern()) <= 0)
                exceptions.Add(new Exception($"ROUTE_PATTERN variable not set properly, available route patterns:\n" + 
                    $"\"{CASE.SINGULAR}\", \"{CASE.PLURAL}\", \"{CASE.SNAKE}\", \"{CASE.SPINAL}\" OR \"{CASE.CAMEL}\""));
            
            if (!(data.Versioned = GetVersioned()))
                exceptions.Add(new Exception("VERSIONED_PATH variable not set properly, available values:\n" + 
                    "\"true\" OR \"false\""));
            
            if ((data.HttpVerbs = GetHttpVerbs()) == null)
                exceptions.Add(new Exception($"HTTP_VERBS variable not set properly, available HTTP verbs:\n" + 
                    $"\"{HTTPVERBS.GET}\", \"{HTTPVERBS.POST}\", \"{HTTPVERBS.PUT}\", \"{HTTPVERBS.DELETE}\"," +
                    $"\"{HTTPVERBS.PATCH}\", \"{HTTPVERBS.OPTIONS}\", \"{HTTPVERBS.HEAD}\""));
            
            if ((data.StatusCode = GetStatusCode()) == null)
                exceptions.Add(new Exception($"STATUS_CODE variable not set properly, example on how to set:\n" + 
                    $"\"{{ \"{HTTPVERBS.GET}\": [200, 500], \"{HTTPVERBS.POST}\": [200, 500], " +
                    $"\"{HTTPVERBS.PUT}\": [200, 500], \"{HTTPVERBS.DELETE}\": [200, 500] }}\""));

            if ((data.PathLevels = GetPathLevels()) < 0)
                exceptions.Add(new Exception("PATH_LEVELS variable not set properly, available level's:\n" + 
                    "minimum: \"0\""));
            
            if ((data.BaseUrl = GetBaseURL()) == null)
                exceptions.Add(new Exception("BASE_URL variable not set properly, example on how to set:\n" + 
                    "BASE_URL: \"yourbaseurl.com\""));
            
            if ((data.SwaggerPath = GetSwaggerPath()) == null)
                exceptions.Add(new Exception("SWAGGER_PATH variable not set properly, example on how to set:\n" + 
                    "SWAGGER_PATH: \"yourswaggerpath.com\""));

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

            return true;
        }

        public LANG GetLanguage() {
            LANG result = LANG.NOT_SET;
            string value = Environment.GetEnvironmentVariable("LANGUAGE");

            if (!Enum.TryParse(value, true, out result) || string.IsNullOrEmpty(value)) {
                return LANG.NOT_SET;
            }

            return result;
        }

        public CASE GetRoutePattern() {
            string value = Environment.GetEnvironmentVariable("ROUTE_PATTERN");
            CASE result = CASE.NOT_SET;

            if (!Enum.TryParse(value, true, out result) || string.IsNullOrEmpty(value)) {
                return CASE.NOT_SET;
            }

            return result;
        }

        public bool GetVersioned() {
            string value = Environment.GetEnvironmentVariable("VERSIONED_PATH");
            bool result;

            if (!bool.TryParse(value, out result) || string.IsNullOrEmpty(value)) {
                return false;
            }
            
            return true;
        }

        public HTTPVERBS[] GetHttpVerbs() {
            string value = Environment.GetEnvironmentVariable("HTTP_VERBS");
            if (string.IsNullOrEmpty(value))
                return null;
            
            string[] verbosHttp = JsonSerializer.Deserialize<string[]>(value);
            HTTPVERBS[] resultArray = new HTTPVERBS[verbosHttp.Length];
            HTTPVERBS result;

            for(int i = 0; i < verbosHttp.Length; i++)
            {
                if (!Enum.TryParse(verbosHttp[i], true, out result))
                    return null;
                
                if (result == HTTPVERBS.NOT_SET)
                    return null;
                
                resultArray[i] = result;
            }

            return resultArray;
        }

        public Dictionary<string, int[]> GetStatusCode() {
            try {
                string value = Environment.GetEnvironmentVariable("STATUS_CODE");

                Dictionary<string, int[]> statusCode = JsonSerializer.Deserialize<Dictionary<string, int[]>>(value);
                return statusCode;
            }
            catch (Exception) {
                return null;
            }
        }

        public int GetPathLevels() {
            try 
            {
                string value = Environment.GetEnvironmentVariable("PATH_LEVELS");
                int result;

                if (!int.TryParse(value, out result)) {
                    return -1;
                }

                return result;
            }
            catch (ArgumentNullException) 
            {
                return -1;
            }
        }

        public string GetBaseURL() {
            try
            {
                string value = Environment.GetEnvironmentVariable("BASE_URL");
                return value;
            }
            catch (ArgumentNullException) 
            {
                return null;
            }
        }

        public string GetSwaggerPath() {
            try {
                string value = Environment.GetEnvironmentVariable("SWAGGER_PATH");
                return value;
            } catch (ArgumentNullException) {
                return null;
            }
        }
    }
}