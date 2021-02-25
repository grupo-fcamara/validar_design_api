using System.Collections.Generic;
using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using App.Entities.Swagger.Two;
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

            var paths = (new string[] { "path", "path/other" }).Select(s => new ApiPath(s));
            var docPaths = documentation.Paths.Select(pair => new ApiPath(pair.Key));

            Assert.True(paths.AllEqual(docPaths));
        }
    }
}