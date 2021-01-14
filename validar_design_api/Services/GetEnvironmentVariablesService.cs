using System;
using System.Text.Json;
using validar_design_api.Entities;
using System.Collections.Generic;

namespace validar_design_api.Services
{
    public class GetEnvironmentVariables : IGetEnvironmentVariables
    {
        public bool Validate(StructuralData data) {
            if ((int) (data.Language = GetLanguage()) <= 0)
                throw new ExceptionError("LANGUAGE variable not set properly, available languages:\n" + 
                                        "\"ENGLISH\" OR \"PORTUGUESE\"");

            if ((int) (data.RoutePattern = GetRoutePattern()) <= 0)
                throw new ExceptionError("ROUTE_PATTERN variable not set properly, available route patterns:\n" + 
                                        "\"SINGULAR\", \"PLURAL\", \"SNAKE\", \"SPINAL\" OR \"CAMEL\"");
            
            if (!(data.Versioned = GetVersioned()))
                throw new ExceptionError("VERSIONED_PATH variable not set properly, available versioned's:\n" + 
                                        "\"true\" OR \"false\"");
            
            if ((data.HttpVerbs = GetHttpVerbs()) == null)
                throw new ExceptionError("HTTP_VERBS variable not set properly, available http verbs::\n" + 
                                        "\"GET\", \"POST\", \"PUT\", \"DELETE\", \"PATCH\", \"OPTIONS\", \"HEAD\"");
            
            if ((data.StatusCode = GetStatusCode()) == null)
                throw new ExceptionError("STATUS_CODE variable not set properly, example on how to set:\n" + 
                                        "\"{ \"GET\": [200, 500], \"POST\": [200, 500], \"PUT\": [200, 500], \"DELETE\": [200, 500] }\"");
            
            if ((data.PathLevels = GetPathLevels()) < 0)
                throw new ExceptionError("PATH_LEVELS variable not set properly, available level's:\n" + 
                                        "minimum: \"0\"");
            
            if ((data.BaseUrl = GetBaseURL()) == null)
                throw new ExceptionError("BASE_URL variable not set properly, example on how to set:\n" + 
                                        "BASE_URL: \"yourbaseurl.com\"");
            
            if ((data.SwaggerPath = GetSwaggerPath()) == null)
                throw new ExceptionError("SWAGGER_PATH variable not set properly, example on how to set:\n" + 
                                        "SWAGGER_PATH: \"yourswaggerpath.com\"");

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
            try
            {
                string value = Environment.GetEnvironmentVariable("SWAGGER_PATH");
                return value;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}