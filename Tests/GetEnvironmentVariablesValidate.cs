using System;
using Xunit;

using App.Entities;
using App.Services;
using App.Services.Exceptions;

namespace Tests 
{
    public class GetEnvironmentVariablesValidate
    {
        [
            Theory,
            InlineData
            (
                new string[] { "LANGUAGE" },
                new string[] { "" },
                "LANGUAGE variable not set properly, available languages:\n\"ENGLISH\" OR \"PORTUGUESE\""
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN" },
                new string[] { "ENGLISH", "" },
                "ROUTE_PATTERN variable not set properly, available route patterns:\n\"SINGULAR\", \"PLURAL\", \"SNAKE\", \"SPINAL\" OR \"CAMEL\""
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH" },
                new string[] { "ENGLISH", "PLURAL", "" },
                "VERSIONED_PATH variable not set properly, available versioned's:\n\"true\" OR \"false\""
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS" },
                new string[] { "ENGLISH", "PLURAL", "false", "" },
                "HTTP_VERBS variable not set properly, available http verbs:\n\"GET\", \"POST\", \"PUT\", \"DELETE\", \"PATCH\", \"OPTIONS\", \"HEAD\""
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE" },
                new string[] { "ENGLISH", "PLURAL", "false", "[\"GET\"]", "" },
                "STATUS_CODE variable not set properly, example on how to set:\n\"{ \"GET\": [200, 500], \"POST\": [200, 500], \"PUT\": [200, 500], \"DELETE\": [200, 500] }\""
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE", "PATH_LEVELS" },
                new string[] { "ENGLISH", "PLURAL", "false", "[\"GET\"]", "{ \"GET\": [200, 500] }", "" },
                "PATH_LEVELS variable not set properly, available level's:\nminimum: \"0\""
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE", "PATH_LEVELS", "BASE_URL" },
                new string[] { "ENGLISH", "PLURAL", "false", "[\"GET\"]", "{ \"GET\": [200, 500] }", "2", "" },
                "BASE_URL variable not set properly, example on how to set:\nBASE_URL: \"yourbaseurl.com\""
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE", "PATH_LEVELS", "BASE_URL", "SWAGGER_PATH" },
                new string[] { "ENGLISH", "PLURAL", "false", "[\"GET\"]", "{ \"GET\": [200, 500] }", "2", "base_url.com", "" },
                "SWAGGER_PATH variable not set properly, example on how to set:\nSWAGGER_PATH: \"yourswaggerpath.com\""
            )
        ]
        public void IsVariableNull(String[] keys, String[] values, string error)
        {
            for(int i = 0; i < keys.Length; i++)
            {
                Environment.SetEnvironmentVariable(keys[i], values[i]);
            }
            
            IGetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();
            StructuralData data = new StructuralData();

            try {
                getEnvironmentVariables.Validate(data);
            } catch(ExceptionError ex) {
                Assert.Equal(ex.errorMsg, error);
            }
        }
    }
}