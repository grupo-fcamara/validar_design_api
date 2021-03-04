using Xunit;
using System.Linq;
using App.Entities.Api;
using App.Entities.Environment;
using App.Services.Validations.Level3;

namespace Tests.Services.Validations.Level3
{
    public class PaginationValidate : Validation
    {
        [Theory]
        [InlineData(2, 
            new string[] { "/home", "/home/jogos", "/home/jogos/{id}" },
            new string[] { "/users", "/users/{id}", "/users/{id}/jogos"}
        )]
        public void ReturnProperly(int expectedProblems, string[] paginatedPaths, string[] notPaginatedPaths)
        {
            var validator = new ValidatePagination();
            
            var endPoints = paginatedPaths.Select(p => CreateEndPoint(p, true)).ToList();
            endPoints.AddRange(notPaginatedPaths.Select(p => CreateEndPoint(p, false)));

            var output = ReturnProblems(validator, endPoints.ToArray());
            Assert.Equal(expectedProblems, output.Problems.Count());
        }

        private EndPoint CreateEndPoint(string path, bool paginated)
        {
            var endPoint = new EndPoint() { Path = new ApiPath(path), Verb = HttpVerbs.GET };
            if (paginated)
                endPoint.Parameters = new EndPointParameter[] { new EndPointParameter("page", "body") };

            return endPoint;
        }
    }
}