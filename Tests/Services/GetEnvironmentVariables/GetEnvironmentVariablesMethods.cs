using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

using App.Entities;
using App.Services;

namespace Tests.Services
{
    public class GetEnvironmentVariablesMethods
    {
        [Theory]
        [InlineData("LANGUAGE", "ENGLISH", Language.ENGLISH)]
        public void ReturnLanguageFromEnvironment(string env, string value, Language expected)
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.Equal(getEnvironmentVariables.GetLanguage(), expected);
        }

        [Theory]
        [InlineData(
            "HTTP_VERBS",
            "[\"GET\", \"POST\", \"PUT\", \"DELETE\", \"OPTIONS\"]", 
            new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST, HttpVerbs.PUT, HttpVerbs.DELETE, HttpVerbs.OPTIONS }
        )]
        public void ReturnHttpVerbFromEnvironment(string env, string value, HttpVerbs[] expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();
            HttpVerbs[] data = getEnvironmentVariables.GetHttpVerbs();

            for(int i = 0; i < data.Length; i++)
            {
                Assert.Equal(data[i], expected[i]);
            }
        }

        [Theory]
        [InlineData("ROUTE_PATTERN", "SNAKE", CasePattern.SNAKE)]
        public void ReturnRoutePatternFromEnvironment(string env, string value, CasePattern expected)
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.Equal(getEnvironmentVariables.GetRoutePattern(), expected);
        }

        [Theory]
        [InlineData("VERSIONED_PATH", "true", true)]
        public void ReturnVersionedPathFromEnvironment(string env, string value, bool expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.Equal(getEnvironmentVariables.IsVersioned(), expected);
        }

        [Theory]
        [InlineData(
            "STATUS_CODE",
            "{ \"GET\": [200, 500], \"POST\": [200, 500], \"PUT\": [200, 500], \"DELETE\": [200, 500] }"
        )]
        public void ReturnStatusCodeFromEnvironment(string env, string value) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            StatusCodePerVerb expected = JsonSerializer.Deserialize<Dictionary<string, int[]>>(value);
            var actual = getEnvironmentVariables.GetStatusCodePerVerb();

            Assert.True(expected.AllEqual(actual));
        }

        [Theory]
        [InlineData("PATH_LEVELS", "4", 4)]
        public void ReturnPathLevelsFromEnvironment(string env, string value, int expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.Equal(getEnvironmentVariables.GetPathLevels(), expected);
        }

        [Theory]
        [InlineData("BASE_URL", "www.fcamara.com.br", "www.fcamara.com.br")]
        public void ReturnBaseUrlFromEnvironment(string env, string value, string expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.Equal(getEnvironmentVariables.GetBaseUrl(), expected);
        }

        [Theory]
        [InlineData("SWAGGER_PATH", "swagger.com", "swagger.com")]
        public void ReturnSwaggerPathFromEnvironment(string env, string value, string expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.Equal(getEnvironmentVariables.GetSwaggerPath(), expected);
        }
    }
}
