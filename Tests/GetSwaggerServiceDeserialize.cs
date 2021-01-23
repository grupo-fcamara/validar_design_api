using System;
using Xunit;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;

using App.Entities;
using App.Services;

namespace Tests
{
    public class GetSwaggerServiceDeserialize
    {
        [Theory, InlineData("hahaha")]
        public void ThrowJsonExceptionIfNotJson(string json)
        {
            Assert.Throws<JsonException>(() => new GetSwaggerService().Deserialize(json));
        }
    }
}
