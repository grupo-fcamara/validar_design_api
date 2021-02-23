using System.Collections.Generic;
using App.Entities.Swagger.Two;
using Xunit;

namespace Tests
{
    public class DocumentationIsValid
    {
        public Documentation DocumentationWithRequiredProps
        {
            get
            {   
                var responses = new Dictionary<string, SwaggerResponse>();
                responses["Key"] = new SwaggerResponse() { Description = "Response" };

                var paths = new Dictionary<string, SwaggerPathItem>();
                paths["/route"] = new SwaggerPathItem
                {
                    Get = new SwaggerOperation { Responses = responses }
                };

                var doc = new Documentation()
                {
                    Swagger = "2.0",
                    Info = new SwaggerInfo
                    {
                        Title = "API",
                        Version = "1.0",
                    },
                    Paths = paths
                };

                return doc;
            }
        }

        [Fact]
        public void IsValidWithRequiredProps()
        {
            var doc = DocumentationWithRequiredProps;
            Assert.True(doc.IsValid);
        }

        [Fact]
        public void IsInvalidWithoutInfo()
        {
            var doc = DocumentationWithRequiredProps;
            doc.Info = null;
            Assert.False(doc.IsValid);
        }
    }
}