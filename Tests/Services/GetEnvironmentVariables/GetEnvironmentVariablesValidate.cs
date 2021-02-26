using System;
using Xunit;

using App.Entities;
using App.Services;

namespace Tests.Services
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
                new string[] { "ROUTE_PATTERN" },
                new string[] { "" },
                1
            ),
            InlineData
            (
                new string[] { "VERSIONED_PATH" },
                new string[] { "" },
                1
            ),
            InlineData
            (
                new string[] { "HTTP_VERBS" },
                new string[] { "" },
                1
            ),
            InlineData
            (
                new string[] { "STATUS_CODE" },
                new string[] { "" },
                1
            ),
            InlineData
            (
                new string[] { "PATH_LEVELS" },
                new string[] { "" },
                1
            ),
            InlineData
            (
                new string[] { "BASE_URL" },
                new string[] { "" },
                1
            ),
            InlineData
            (
                new string[] { "SWAGGER_PATH" },
                new string[] { "" },
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

            StructuralData data;

            try {
                data = new GetEnvironmentVariables().GetData();
            } catch(AggregateException ex) {
                Assert.Equal(expectedErrors, ex.InnerExceptions.Count);
            }
        }

        private void SetDefaults()
        {
            Environment.SetEnvironmentVariable("LANGUAGE", "ENGLISH");
            Environment.SetEnvironmentVariable("ROUTE_PATTERN", "CAMEL");
            Environment.SetEnvironmentVariable("PLURAL", "true");
            Environment.SetEnvironmentVariable("VERSIONED_PATH", "true");
            Environment.SetEnvironmentVariable("HTTP_VERBS", "[\"GET\"]");
            Environment.SetEnvironmentVariable("STATUS_CODE", "{ \"GET\": [200, 500] }");
            Environment.SetEnvironmentVariable("PATH_LEVELS", "2");
            Environment.SetEnvironmentVariable("BASE_URL", "http://5de67ce5-kmad-veiculobacke-48f3-305261353.us-east-1.elb.amazonaws.com");
            Environment.SetEnvironmentVariable("SWAGGER_PATH", "http://5de67ce5-torre-veiculorota-0aa0-481195426.us-east-1.elb.amazonaws.com/v2/api-docs");
        }
    }
}