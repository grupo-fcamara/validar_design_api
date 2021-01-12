using System;
using Xunit;
using validar_design_api;
using System.Linq;

namespace valida_design_api.tests
{
    public class IsEnvironmentVariableWorking
    {
        [Theory]
        [InlineData("IDIOMA", "ENGLISH", Language.ENGLISH)]
        public void ReturnLanguageFromEnvironment(string env, string value, Language expected)
        {
            Environment.SetEnvironmentVariable(env, value);
            LanguageVariable language = new LanguageVariable();
            Assert.True(Object.Equals(language.Value, expected));
        }

        [
            Theory,
            InlineData(
                "VERBOS_HTTP",
                "[\"GET\", \"POST\", \"PUT\", \"DELETE\", \"OPTIONS\"]", 
                new EHttpVerbs[] { EHttpVerbs.GET, EHttpVerbs.POST, EHttpVerbs.PUT, EHttpVerbs.DELETE, EHttpVerbs.OPTIONS }
            )
        ]
        public void ReturnHttpVerbFromEnvironment(string env, string value, EHttpVerbs[] expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            HttpVerbs httpVerbs = new HttpVerbs();
            Assert.True(Enumerable.SequenceEqual(httpVerbs.Value, expected));
        }

        [
            Theory,
            InlineData("PADRAO_ROTAS", "SNAKE", Case.SNAKE)
        ]
        public void ReturnRoutePatternFromEnvironment(string env, string value, Case expected)
        {
            Environment.SetEnvironmentVariable(env, value);
            RoutePattern rotas = new RoutePattern();
            Assert.True(Object.Equals(rotas.Value, expected));

        }

        [
            Theory,
            InlineData("PATH_VERSIONADO", "true", true)
        ]
        public void ReturnVersionedPathFromEnvironment(string env, string value, bool expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            Versioned versioned = new Versioned();
            Assert.Equal(versioned.Value, expected); 
        }

        [
            Theory,
            InlineData(
                "STATUS_CODE",
                "{ \"GET\": [200, 500], \"POST\": [200, 500], \"PUT\": [200, 500], \"DELETE\": [200, 500] }",
                new int[] {200, 500}, new int[] {200, 500}, new int[] {200, 500}, new int[] {200, 500}
            )
        ]
        public void ReturnStatusCodeFromEnvironment(string env, string value, int[] get, int[] post, int[] put, int[] delete) 
        {
            Environment.SetEnvironmentVariable(env, value);
            StatusCodeVariable statusCode = new StatusCodeVariable();
            Assert.True(Enumerable.SequenceEqual(statusCode.Value.GET, get));
            Assert.True(Enumerable.SequenceEqual(statusCode.Value.POST, post));
            Assert.True(Enumerable.SequenceEqual(statusCode.Value.PUT, put));
            Assert.True(Enumerable.SequenceEqual(statusCode.Value.DELETE, delete));
        }

        [
            Theory, 
            InlineData("NIVEIS_PATH", "4", 4)
        ]
        public void ReturnPathLevelsFromEnvironment(string env, string value, int expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            PathLevels pathLevels = new PathLevels();
            Assert.Equal(pathLevels.Value, expected);
        }

        [
            Theory,
            InlineData("BASE_URL", "www.fcamara.com.br", "www.fcamara.com.br")
        ]
        public void ReturnBaseUrlFromEnvironment(string env, string value, string expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            BaseUrl baseUrl = new BaseUrl();
            Assert.Equal(baseUrl.Value, expected);
        }

        [
            Theory, 
            InlineData("PATH_SWAGGER", "swagger.com", "swagger.com")
        ]
        public void ReturnSwaggerPathFromEnvironment(string env, string value, string expected) 
        {
            Environment.SetEnvironmentVariable(env, value);
            SwaggerPath swaggerPath = new SwaggerPath();
            Assert.Equal(swaggerPath.Value, expected);
        }
    }
}
