using System.Collections.Generic;
using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using App.Entities.Swagger.Two;
using Tests.Util;
using Xunit;

namespace Tests.Entities.Swagger.Two
{
    public class DocumentationPathsImplementation
    {
        [Fact]
        public void ReturnProperly()
        {
            var documentation = new Documentation();
            documentation.Paths = new Dictionary<string, SwaggerPathItem>();
            
            documentation.Paths["path/"] = new SwaggerPathItem
            { 
                Get = new SwaggerOperation(),
                Post = new SwaggerOperation(),
                Put = new SwaggerOperation()
            };

            documentation.Paths["path/other"] = new SwaggerPathItem
            {
                Head = new SwaggerOperation()
            };

            IDocumentation doc = documentation;

            var expected = (new string[] { "path", "path/other" }).Select(s => new ApiPath(s));
            var actual = documentation.Paths.Select(pair => new ApiPath(pair.Key));

            AssertUtil.AllEqual(expected, actual);
        }
    }
}