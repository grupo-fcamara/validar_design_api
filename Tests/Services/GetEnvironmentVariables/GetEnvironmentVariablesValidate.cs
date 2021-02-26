using System;
using Xunit;
using App.Services;

namespace Tests.Services
{
    public class GetEnvironmentVariablesValidate
    {
        [Theory]
        [InlineData
        (
            "LANGUAGE", "ROUTE_PATTERN", "PLURAL",
            "VERSIONED_PATH", "HTTP_VERBS", "STATUS_CODE",
            "PATH_LEVELS", "BASE_URL", "SWAGGER_PATH"
        )]
        public void ThrowErrorIfNullOrEmpty(params string[] variables)
        {
            foreach (var variable in variables)
            {
                SetDefaults();
                Environment.SetEnvironmentVariable(variable, "");
                Assert.ThrowsAny<Exception>(() => new GetEnvironmentVariables().GetData());

                Environment.SetEnvironmentVariable(variable, null);
                Assert.ThrowsAny<Exception>(() => new GetEnvironmentVariables().GetData());
            }         
        }

        [Theory]
        [InlineData("LANGUAGE", "NOT_A_LANGUAGE")]
        [InlineData("ROUTE_PATTERN", "NOT_A_PATTERN")]
        [InlineData("PLURAL", "NOT_A_BOOLEAN")]
        [InlineData("VERSIONED_PATH", "NOT_A_BOOLEAN")]
        [InlineData("HTTP_VERBS", "NOT_A_VERB")]
        [InlineData("STATUS_CODE", "INVALID")]
        [InlineData("PATH_LEVELS", "STRING")]
        [InlineData("PATH_LEVELS", "1.5")]
        [InlineData("PATH_LEVELS", "-1")]
        public void ThrowErrorIfInvalid(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value);
            Assert.ThrowsAny<Exception>(() => new GetEnvironmentVariables().GetData());
        }

        private void SetDefaults()
        {
            Environment.SetEnvironmentVariable("LANGUAGE", "ENGLISH");
            Environment.SetEnvironmentVariable("ROUTE_PATTERN", "PLURAL");
            Environment.SetEnvironmentVariable("PLURAL", "true");
            Environment.SetEnvironmentVariable("VERSIONED_PATH", "true");
            Environment.SetEnvironmentVariable("HTTP_VERBS", "[\"GET\"]");
            Environment.SetEnvironmentVariable("STATUS_CODE", "{ \"GET\": [200, 500] }");
            Environment.SetEnvironmentVariable("PATH_LEVELS", "2");
            Environment.SetEnvironmentVariable("BASE_URL", "http://squad5-fifo-api.herokuapp.com/api/");
            Environment.SetEnvironmentVariable("SWAGGER_PATH", "http://squad5-fifo-api.herokuapp.com/api/v2/api-docs");
        }
    }
}