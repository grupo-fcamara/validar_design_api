using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities;
using App.Entities.Swagger.Two;
using System.Collections.Generic;
using App.Entities.Swagger;

namespace Tests.Services.Validations.Level3
{
    public class StatusCodeValidate : Validation
    {
        [Fact]
        public void ReturnProperly()
        {
            var endPoints = new List<EndPoint>();
            endPoints.Add(CreateEndPoint("1", HTTPVERBS.GET, 200, 404));
            endPoints.Add(CreateEndPoint("2", HTTPVERBS.GET, 200, 500));
            endPoints.Add(CreateEndPoint("3", HTTPVERBS.POST, 200));
            endPoints.Add(CreateEndPoint("4", HTTPVERBS.POST, 200, 500, 404));
            endPoints.Add(CreateEndPoint("5", HTTPVERBS.PUT));

            var allowedCodes = StatusCodePerVerb.Empty;
            allowedCodes[HTTPVERBS.GET] = new int[] {200, 404};
            allowedCodes[HTTPVERBS.POST] = new int[] {200, 500};
            allowedCodes[HTTPVERBS.PUT] = new int[] {300, 504};

            var output = ReturnProblems(new ValidateStatusCode(allowedCodes), endPoints.ToArray());
            Assert.Equal(2, output.Problems.Count());
        }

        private EndPoint CreateEndPoint(string path, HTTPVERBS verb, params int[] responses)
        {
            return new EndPoint()
            {
                Path = new ApiPath(path),
                Verb = verb,
                Responses = responses
            };
        }
    }
}