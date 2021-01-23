using System;
using Xunit;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;

using App.Entities;
using App.Services;

namespace Tests
{
    public class GetSwaggerServiceGetByUrl
    {
        [Theory]
        [InlineData("https://squad5-fifo-api.herokuapp.com/api/v2/api-docs")]
        public void GetByUrl(string url)
        {
            var doc = new GetSwaggerService().GetByUrl(url);
        }

        [Theory, InlineData("hahaha")]
        public void ThrowWebExceptionIfNotUrl(string url)
        {
            Assert.Throws<WebException>(() => new GetSwaggerService().GetByUrl(url));
        }
    }
}
