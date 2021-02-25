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

            var endPoints = new List<EndPoint>();
            endPoints.AddRange(EndPoint.Create("path/", HttpVerbs.GET, HttpVerbs.POST, HttpVerbs.PUT));
            endPoints.Add(new EndPoint(new ApiPath("path/other"), HttpVerbs.HEAD));

            Assert.True(endPoints.AllEqual(documentation.EndPoints));
        }
    }
}