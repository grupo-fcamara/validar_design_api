using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities;
using App.Entities.Swagger.Two;
using App.Entities.Swagger;
using System.Collections.Generic;

namespace Tests.Services.Validations.Level3
{
    public class HttpVerbsValidate : Validation
    {
        [Theory]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET }, new HTTPVERBS[] { HTTPVERBS.GET }, 0)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET }, new HTTPVERBS[] { HTTPVERBS.POST }, 1)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.POST }, new HTTPVERBS[] { HTTPVERBS.PUT }, 1)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET }, new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, 1)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, 0)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST, HTTPVERBS.PUT }, 1)]
        public void ReturnProperly(HTTPVERBS[] allowedVerbs, HTTPVERBS[] verbs, int expectedProblems)
        {
            var documentation = new DocumentationForTests();
            documentation.EndPoints = GetMultipleEndPoints("path/", verbs);

            var output = new ValidatePathHttpVerbs(allowedVerbs).Validate(documentation);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}