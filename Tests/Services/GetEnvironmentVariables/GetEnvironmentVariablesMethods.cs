using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using App.Entities.Environment;
using App.Services.Environment;
using Xunit;
using Tests.Util;

namespace Tests.Services
{
    public class GetEnvironmentVariablesMethods
    {
        [Theory]
        [InlineData("ENGLISH", Language.ENGLISH)]
        public void ReturnLanguageFromEnvironment(string value, Language expected)
        {
            Environment.SetEnvironmentVariable("LANGUAGE", value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            Assert.Equal(expected, getEnvironmentVariables.GetLanguage());
        }

        [Theory]
        [InlineData(
            "[\"GET\", \"POST\", \"PUT\", \"DELETE\", \"OPTIONS\"]", 
            HttpVerbs.GET, HttpVerbs.POST, HttpVerbs.PUT, HttpVerbs.DELETE, HttpVerbs.OPTIONS
        )]
        public void ReturnHttpVerbFromEnvironment(string value, params HttpVerbs[] expected) 
        {
            Environment.SetEnvironmentVariable("HTTP_VERBS", value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();
            HttpVerbs[] data = getEnvironmentVariables.GetHttpVerbs();

            for(int i = 0; i < data.Length; i++)
            {
                Assert.Equal(expected[i], data[i]);
            }
        }

        [Theory]
        [InlineData("SNAKE", CasePattern.SNAKE)]
        public void ReturnRoutePatternFromEnvironment(string value, CasePattern expected)
        {
            Environment.SetEnvironmentVariable("ROUTE_PATTERN", value);
            Assert.Equal(expected, new GetEnvironmentVariables().GetRoutePattern());
        }

        [Theory]
        [InlineData("true", true)]
        public void ReturnVersionedPathFromEnvironment(string value, bool expected) 
        {
            Environment.SetEnvironmentVariable("VERSIONED_PATH", value);
            Assert.Equal(expected, new GetEnvironmentVariables().IsVersioned());
        }

        [Theory]
        [InlineData(
            "{ \"GET\": [200, 500], \"POST\": [200, 500], \"PUT\": [200, 500], \"DELETE\": [200, 500] }"
        )]
        public void ReturnStatusCodeFromEnvironment(string value) 
        {
            Environment.SetEnvironmentVariable("STATUS_CODE", value);
            GetEnvironmentVariables getEnvironmentVariables = new GetEnvironmentVariables();

            StatusCodePerVerb expected = JsonSerializer.Deserialize<Dictionary<string, int[]>>(value);
            var actual = getEnvironmentVariables.GetStatusCodePerVerb();

            Assert.Equal(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                Assert.Equal(expected.ElementAt(i).Key, actual.ElementAt(i).Key);
                AssertUtil.AllEqual(expected.ElementAt(i).Value, actual.ElementAt(i).Value);
            }
        }

        [Theory]
        [InlineData("4", 4)]
        public void ReturnPathLevelsFromEnvironment(string value, int expected) 
        {
            Environment.SetEnvironmentVariable("PATH_LEVELS", value);
            Assert.Equal(expected, new GetEnvironmentVariables().GetPathLevels());
        }

        [Theory]
        [InlineData("www.fcamara.com.br")]
        public void ReturnBaseUrlFromEnvironment(string value) 
        {
            Environment.SetEnvironmentVariable("BASE_URL", value);
            Assert.Equal(value, new GetEnvironmentVariables().GetBaseUrl());
        }

        [Theory]
        [InlineData("swagger.com")]
        public void ReturnSwaggerPathFromEnvironment(string value) 
        {
            Environment.SetEnvironmentVariable("SWAGGER_PATH", value);
            Assert.Equal(value, new GetEnvironmentVariables().GetSwaggerPath());
        }
    }
}
