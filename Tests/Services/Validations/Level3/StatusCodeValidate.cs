using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities;
using System.Collections.Generic;

namespace Tests.Services.Validations.Level3
{
    public class StatusCodeValidate : Validation
    {
        [Fact]
        public void ReturnProperly()
        {
            var endPoints = new List<EndPoint>();

            endPoints.Add(CreateEndPoint("1", HttpVerbs.GET, 200, 404));
            endPoints.Add(CreateEndPoint("2", HttpVerbs.GET, 200, 500));
            endPoints.Add(CreateEndPoint("3", HttpVerbs.POST, 200));
            endPoints.Add(CreateEndPoint("4", HttpVerbs.POST, 200, 500, 404));
            endPoints.Add(CreateEndPoint("5", HttpVerbs.PUT));

            var allowedCodes = StatusCodePerVerb.Empty;
            allowedCodes[HttpVerbs.GET] = new int[] {200, 404};
            allowedCodes[HttpVerbs.POST] = new int[] {200, 500};
            allowedCodes[HttpVerbs.PUT] = new int[] {300, 504};

            var output = ReturnProblems(new ValidateStatusCode(allowedCodes), endPoints.ToArray());
            Assert.Equal(2, output.Problems.Count());
        }

        private EndPoint CreateEndPoint(string path, HttpVerbs verb, params int[] responses)
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