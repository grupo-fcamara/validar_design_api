using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities;
using App.Entities.Swagger.Two;
using System.Collections.Generic;
using App.Entities.Swagger;

namespace Tests.Services.Validations.Level3
{
    public class StatusCodeValidate
    {
        [Fact]
        public void ReturnProperly()
        {            
            var documentation = new DocumentationForTests();
            var endPoints = new List<EndPoint>();

            endPoints.Add(new EndPoint() { Verb = HttpVerbs.GET, Responses = new int[] { 200, 404 } });
            endPoints.Add(new EndPoint() { Verb = HttpVerbs.GET, Responses = new int[] { 200, 500 } });
            endPoints.Add(new EndPoint() { Verb = HttpVerbs.POST, Responses = new int[] { 200 } });
            endPoints.Add(new EndPoint() { Verb = HttpVerbs.POST, Responses = new int[] { 200, 500, 404 } });
            endPoints.Add(new EndPoint() { Verb = HttpVerbs.PUT, Responses = new int[] { } });
            documentation.EndPoints = endPoints.ToArray();

            var allowedCodes = StatusCodePerVerb.Empty;
            allowedCodes[HttpVerbs.GET] = new int[] {200, 404};
            allowedCodes[HttpVerbs.POST] = new int[] {200, 500};
            allowedCodes[HttpVerbs.PUT] = new int[] {300, 504};

            var output = new ValidateStatusCode(StatusCodePerVerb.Empty).Validate(documentation);
            Assert.Equal(2, output.Problems.Count());
        }
    }
}