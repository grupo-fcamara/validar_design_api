using Xunit;
using System.Linq;
using App.Services.Validations.Level1;
using App.Entities;
using App.Entities.Swagger;

namespace Tests.Services.Validations.Level1
{
    public class GetRoutesPerPathValidate : Validation
    {
        [Fact]
        public void ReturnProperly()
        {
            var endPoints = EndPoint.Create(HttpVerbs.GET,
                "users/", "users/{id}", "users/online", "cars/", "cars/{id}", "pets/", "pets/{id}"
            ).ToList();
            endPoints.Add(new EndPoint(new ApiPath("cars/broken"), HttpVerbs.POST));     

            var output = ReturnProblems(new ValidateGetRoutesPerPath(), endPoints.ToArray());
            Assert.Equal(1, output.Problems.Count());
        }
    }
}