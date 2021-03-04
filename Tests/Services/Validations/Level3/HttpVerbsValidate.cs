using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities.Environment;
using App.Entities.Api;

namespace Tests.Services.Validations.Level3
{
    public class HttpVerbsValidate : Validation
    {
        [Theory]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET }, new HttpVerbs[] { HttpVerbs.GET }, 0)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET }, new HttpVerbs[] { HttpVerbs.POST }, 1)]
        [InlineData(new HttpVerbs[] { HttpVerbs.POST }, new HttpVerbs[] { HttpVerbs.PUT }, 1)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET }, new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, 1)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, 0)]
        [InlineData(new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST }, new HttpVerbs[] { HttpVerbs.GET, HttpVerbs.POST, HttpVerbs.PUT }, 1)]
        public void ReturnProperly(HttpVerbs[] allowedVerbs, HttpVerbs[] verbs, int expectedProblems)
        {
            var validator = new ValidatePathHttpVerbs(allowedVerbs);
            var endPoints = EndPoint.Create("path/", verbs);

            var output = ReturnProblems(validator, endPoints);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}