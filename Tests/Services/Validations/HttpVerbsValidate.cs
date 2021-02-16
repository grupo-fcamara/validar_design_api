using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities;
using App.Entities.Swagger;

namespace Tests
{
    public class HttpVerbsValidate
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
            var documentation = new Documentation();
            documentation.Paths = new System.Collections.Generic.Dictionary<string, SwaggerPathItem>();
            documentation.Paths["path/"] = new SwaggerPathItem();

            if (verbs.Contains(HTTPVERBS.GET)) { documentation.Paths["path/"].Get = new SwaggerOperation(); }
            if (verbs.Contains(HTTPVERBS.PUT)) { documentation.Paths["path/"].Put = new SwaggerOperation(); }
            if (verbs.Contains(HTTPVERBS.POST)) { documentation.Paths["path/"].Post = new SwaggerOperation(); }
            if (verbs.Contains(HTTPVERBS.DELETE)) { documentation.Paths["path/"].Delete = new SwaggerOperation(); }
            if (verbs.Contains(HTTPVERBS.OPTIONS)) { documentation.Paths["path/"].Options = new SwaggerOperation(); }
            if (verbs.Contains(HTTPVERBS.HEAD)) { documentation.Paths["path/"].Head = new SwaggerOperation(); }
            if (verbs.Contains(HTTPVERBS.PATCH)) { documentation.Paths["path/"].Patch = new SwaggerOperation(); }

            var output = new ValidatePathHttpVerbs(allowedVerbs).Validate(documentation);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}