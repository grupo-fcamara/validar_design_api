using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities;
using App.Entities.Swagger.Two;

namespace Tests.Services.Validations.Level3
{
    public class HttpVerbsValidate
    {
        [Theory]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET }, new HttpVerbs[] { HttpVerbs.GET }, 0)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET }, new HttpVerbs[] { HttpVerbs.POST }, 1)]
        [InlineData(new HttpVerbs[] { HttpVerbs.POST }, new HttpVerbs[] { HttpVerbs.PUT }, 1)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET }, new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, 1)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, 0)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST, HttpVerbs.PUT }, 1)]
        public void ReturnProperly(HttpVerbs[] allowedVerbs, HttpVerbs[] verbs, int expectedProblems)
        {
            var documentation = new Documentation();
            documentation.Paths = new System.Collections.Generic.Dictionary<string, SwaggerPathItem>();
            documentation.Paths["path/"] = new SwaggerPathItem();

            if (verbs.Contains(HttpVerbs.GET)) { documentation.Paths["path/"].Get = new SwaggerOperation(); }
            if (verbs.Contains(HttpVerbs.PUT)) { documentation.Paths["path/"].Put = new SwaggerOperation(); }
            if (verbs.Contains(HttpVerbs.POST)) { documentation.Paths["path/"].Post = new SwaggerOperation(); }
            if (verbs.Contains(HttpVerbs.DELETE)) { documentation.Paths["path/"].Delete = new SwaggerOperation(); }
            if (verbs.Contains(HttpVerbs.OPTIONS)) { documentation.Paths["path/"].Options = new SwaggerOperation(); }
            if (verbs.Contains(HttpVerbs.HEAD)) { documentation.Paths["path/"].Head = new SwaggerOperation(); }
            if (verbs.Contains(HttpVerbs.PATCH)) { documentation.Paths["path/"].Patch = new SwaggerOperation(); }

            var output = new ValidatePathHttpVerbs(allowedVerbs).Validate(documentation);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}