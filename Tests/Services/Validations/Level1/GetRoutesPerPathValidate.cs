using Xunit;
using System.Linq;
using App.Services.Validations.Level1;
using App.Entities;
using App.Entities.Swagger;
using System.Collections.Generic;

namespace Tests.Services.Validations.Level1
{
    public class GetRoutesPerPathValidate : Validation
    {
        [Fact]
        public void ReturnProperly()
        {
            var documentation = new DocumentationForTests();
            var endPoints = new List<EndPoint>();

            endPoints.AddRange(EndPoint.Create(HTTPVERBS.GET,
                "users/", "users/{id}", "users/online", "cars/", "cars/{id}", "pets/", "pets/{id}"
            ));
            endPoints.Add(new EndPoint(new ApiPath("cars/broken"), HTTPVERBS.POST));
            
            documentation.EndPoints = endPoints.ToArray();
            var output = new ValidateGetRoutesPerPath().Validate(documentation);
            Assert.Equal(1, output.Problems.Count());
        }
    }
}