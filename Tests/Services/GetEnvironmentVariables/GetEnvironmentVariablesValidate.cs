using System;
using Xunit;

using App.Entities;
using App.Services;

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
                1
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN" },
                new string[] { "ENGLISH", "" },
                1
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH" },
                new string[] { "ENGLISH", "CAMEL", "" },
                1
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS" },
                new string[] { "ENGLISH", "CAMEL", "false", "" },
                1
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE" },
                new string[] { "ENGLISH", "CAMEL", "false", "[\"GET\"]", "" },
                1
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE", "PATH_LEVELS" },
                new string[] { "ENGLISH", "CAMEL", "false", "[\"GET\"]", "{ \"GET\": [200, 500] }", "" },
                1
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE", "PATH_LEVELS", "BASE_URL" },
                new string[] { "ENGLISH", "CAMEL", "false", "[\"GET\"]", "{ \"GET\": [200, 500] }", "2", "" },
                1
            ),
            InlineData
            (
                new string[] { "LANGUAGE", "ROUTE_PATTERN", "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE", "PATH_LEVELS", "BASE_URL", "SWAGGER_PATH" },
                new string[] { "ENGLISH", "CAMEL", "false", "[\"GET\"]", "{ \"GET\": [200, 500] }", "2", "base_url.com", "" },
                1
            )
        ]
        public void IsVariableNull(string[] keys, string[] values, int expectedErrors)
        {
            SetDefaults();
            for(int i = 0; i < keys.Length; i++)
            {
                Environment.SetEnvironmentVariable(keys[i], values[i]);
            }
            
            IGetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();
            StructuralData data = new StructuralData();

            try {
                getEnvironmentVariables.Validate(data);
            } catch(AggregateException ex) {
                Assert.Equal(ex.InnerExceptions.Count, expectedErrors);
            }
        }

        private void SetDefaults()
        {
            Environment.SetEnvironmentVariable("LANGUAGE", "ENGLISH");
            Environment.SetEnvironmentVariable("ROUTE_PATTERN", "PLURAL");
            Environment.SetEnvironmentVariable("VERSIONED_PATH", "true");
            Environment.SetEnvironmentVariable("HTTP_VERBS", "[\"GET\"]");
            Environment.SetEnvironmentVariable("STATUS_CODE", "{ \"GET\": [200, 500] }");
            Environment.SetEnvironmentVariable("PATH_LEVELS", "2");
            Environment.SetEnvironmentVariable("BASE_URL", "http://squad5-fifo-api.herokuapp.com/api/");
            Environment.SetEnvironmentVariable("SWAGGER_PATH", "http://squad5-fifo-api.herokuapp.com/api/v2/api-docs");
        }
    }
}