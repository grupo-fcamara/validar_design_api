using Xunit;
using System.Linq;
using System.Collections.Generic;
using App.Entities.Swagger;
using App.Services.Validations.Level1;

namespace Tests
{
    public class ValidateIdentifier
    {
        [Theory]
        [InlineData(1, "pets/{id}", "pets/{name}", "pets/{id}/users/{id}")]
        public void ReturnProperly(int expectedProblems, params string[] paths)
        {
            var doc = new Documentation();
            doc.Paths = new Dictionary<string, SwaggerPathItem>();
            
            foreach (var path in paths)
            {
                doc.Paths[path] = new SwaggerPathItem();
            }

            var validator = new ValidateIdentifiers();
            var output = validator.Validate(doc);

            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}