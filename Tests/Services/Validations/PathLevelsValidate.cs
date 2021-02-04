using Xunit;
using System.Linq;
using System.Collections.Generic;
using App.Entities.Swagger;
using App.Services.Validations.Level1;

namespace Tests
{
    public class PathLevelsValidate
    {
        [Theory]
        [InlineData(2, 2, "users/{id}/pets/{id}", "countries/{id}/teams/{id}/players", "countries/{id}/teams/{id}/players/{id}")]
        public void ReturnProperly(int expectedProblems, int levelsLimit, params string[] paths)
        {
            var doc = new Documentation();
            doc.Paths = new Dictionary<string, SwaggerPathItem>();
            
            foreach (var path in paths)
            {
                doc.Paths[path] = new SwaggerPathItem();
            }

            var validator = new ValidatePathLevels(levelsLimit);
            var output = validator.Validate(doc);

            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}