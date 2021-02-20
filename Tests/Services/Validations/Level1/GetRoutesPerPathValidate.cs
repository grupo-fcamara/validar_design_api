using Xunit;
using System.Linq;
using App.Services.Validations.Level1;
using App.Entities.Swagger.Two;

namespace Tests.Services.Validations.Level1
{
    public class GetRoutesPerPathValidate : ValidatePaths
    {
        [Fact]
        public void ReturnProperly()
        {
            var pathWithGet = new SwaggerPathItem() { Get = new SwaggerOperation() };
            var emptyPath = new SwaggerPathItem();

            var documentation = new Documentation();
            documentation.Paths = new System.Collections.Generic.Dictionary<string, SwaggerPathItem>()
            {
                {"users/", pathWithGet },
                {"users/{id}", pathWithGet },
                {"users/online", pathWithGet },
                {"cars/", pathWithGet },
                {"cars/{id}", pathWithGet },
                {"cars/broken", emptyPath },
                {"pets/", pathWithGet },
                {"pets/{id}", pathWithGet }
            };

            var output = new ValidateGetRoutesPerPath().Validate(documentation);
            Assert.Equal(1, output.Problems.Count());
        }
    }
}