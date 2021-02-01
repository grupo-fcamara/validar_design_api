using System;
using Xunit;
using System.Collections.Generic;
using System.Text.Json;

using App.Entities;
using App.Services;

namespace Tests
{
    public class GetEnvironmentVariablesMethods
    {
        [ 
            Theory, 
            InlineData("LANGUAGE", "ENGLISH", LANG.ENGLISH)
        ]
        public void ReturnLanguageFromEnvironment(string env, string value, LANG expected)
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.True(Object.Equals(getEnvironmentVariables.GetLanguage(), expected));
        }

        [
            Theory,
            InlineData(
                "HTTP_VERBS",
                "[\"GET\", \"POST\", \"PUT\", \"DELETE\", \"OPTIONS\"]", 
                new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST, HTTPVERBS.PUT, HTTPVERBS.DELETE, HTTPVERBS.OPTIONS }
            )
        ]
        public void ReturnHttpVerbFromEnvironment(string env, string value, HTTPVERBS[] expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();
            HTTPVERBS[] data = getEnvironmentVariables.GetHttpVerbs();

            for(int i = 0; i < data.Length; i++)
            {
                Assert.Equal(data[i], expected[i]);
            }
        }

        [
            Theory,
            InlineData("ROUTE_PATTERN", "SNAKE", CASE.SNAKE)
        ]
        public void ReturnRoutePatternFromEnvironment(string env, string value, CASE expected)
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.True(Object.Equals(getEnvironmentVariables.GetRoutePattern(), expected));
        }

        [
            Theory,
            InlineData("VERSIONED_PATH", "true", true)
        ]
        public void ReturnVersionedPathFromEnvironment(string env, string value, bool expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.True(Object.Equals(getEnvironmentVariables.GetVersioned(), expected));
        }

        [
            Theory,
            InlineData(
                "STATUS_CODE",
                "{ \"GET\": [200, 500], \"POST\": [200, 500], \"PUT\": [200, 500], \"DELETE\": [200, 500] }"
            )   
        ]
        public void ReturnStatusCodeFromEnvironment(string env, string value) 
        {
            Environment.SetEnvironmentVariable(env, value);
            StructuralData data = new StructuralData();
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Dictionary<string, int[]> expected = JsonSerializer.Deserialize<Dictionary<string, int[]>>(value);
            data.StatusCode = getEnvironmentVariables.GetStatusCode();

            foreach (var verb in data.StatusCode) {
                Assert.Equal(data.StatusCode[verb.Key], expected[verb.Key]);
            }
        }

        [
            Theory, 
            InlineData("PATH_LEVELS", "4", 4)
        ]
        public void ReturnPathLevelsFromEnvironment(string env, string value, int expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.True(Object.Equals(getEnvironmentVariables.GetPathLevels(), expected));
        }

        [
            Theory,
            InlineData("BASE_URL", "www.fcamara.com.br", "www.fcamara.com.br")
        ]
        public void ReturnBaseUrlFromEnvironment(string env, string value, string expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.True(Object.Equals(getEnvironmentVariables.GetBaseURL(), expected));
        }

        [
            Theory, 
            InlineData("SWAGGER_PATH", "swagger.com", "swagger.com")
        ]
        public void ReturnSwaggerPathFromEnvironment(string env, string value, string expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.True(Object.Equals(getEnvironmentVariables.GetSwaggerPath(), expected));
        }
    }
}
