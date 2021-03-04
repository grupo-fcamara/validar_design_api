using Xunit;
using System.Linq;
using App.Entities.Api;
using App.Entities.Environment;
using App.Services.Validations.Level3;

namespace Tests.Services.Validations.Level3
{
    public class OrdinationValidate : Validation
    {
        [Theory]
        [InlineData(2, 
            new string[] { "/home", "/home/jogos", "/home/jogos/{id}" },
            new string[] { "/users", "/users/{id}", "/users/{id}/jogos"}
        )]
        public void ReturnProperly(int expectedProblems, string[] orderedPaths, string[] notOrderedPaths)
        {
            var validator = new ValidatePagination();
            
            var endPoints = orderedPaths.Select(p => CreateEndPoint(p, true)).ToList();
            endPoints.AddRange(notOrderedPaths.Select(p => CreateEndPoint(p, false)));

            Assert.Equal(4, endPoints.Count(e => e.Parameters.Any()));
            var output = ReturnProblems(validator, endPoints.ToArray());
            Assert.Equal(expectedProblems, output.Problems.Count());
        }

        private EndPoint CreateEndPoint(string path, bool ordered)
        {
            var endPoint = new EndPoint() { Path = new ApiPath(path), Verb = HttpVerbs.GET };
            if (ordered)
                endPoint.Parameters = new EndPointParameter[] { new EndPointParameter("ascending", "body") };

            return endPoint;
        }
    }
}