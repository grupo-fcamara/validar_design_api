using System;
using Xunit;
using System.Text.Json;
using App.Services;
using System.IO;

namespace Tests.Services
{
    public class GetSwaggerServiceDeserialize
    {
        public string ExamplesPath => Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + @"\SwaggerExamples";

        [Fact]
        public void Deserialize()
        {
            string json = File.ReadAllText(ExamplesPath + @"\2.0.json");
            new GetSwaggerService().Deserialize(json);
        }

        [Fact]
        public void ThrowExceptionIfNotSwagger2()
        {
            string json = File.ReadAllText(ExamplesPath + @"\3.0.json");
            Assert.Throws<Exception>(() => new GetSwaggerService().Deserialize(json));
        }

        [Theory, InlineData("hahaha")]
        public void ThrowJsonExceptionIfNotJson(string json)
        {
            Assert.Throws<JsonException>(() => new GetSwaggerService().Deserialize(json));
        }
    }
}
