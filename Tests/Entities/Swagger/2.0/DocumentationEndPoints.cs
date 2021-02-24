using System.Collections.Generic;
using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using App.Entities.Swagger.Two;
using Xunit;

namespace Tests.Entities.Swagger.Two
{
    public class DocumentationEndPoints
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

            var endPoints = new List<EndPoint>
            {
                new EndPoint() { Path = new ApiPath("path/"), Verb = HTTPVERBS.GET },
                new EndPoint() { Path = new ApiPath("path/"), Verb = HTTPVERBS.POST },
                new EndPoint() { Path = new ApiPath("path/"), Verb = HTTPVERBS.PUT },
                new EndPoint() { Path = new ApiPath("path/other"), Verb = HTTPVERBS.HEAD },
            };

            IDocumentation doc = documentation;

            var toString1 = documentation.EndPoints.Select(e => e.ToString());
            var toString2 = endPoints.Select(e => e.ToString());

            Assert.True(toString1.Equal(toString2));
        }
    }
}