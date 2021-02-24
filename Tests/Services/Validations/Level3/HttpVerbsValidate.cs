using Xunit;
using System.Linq;
using App.Services.Validations.Level3;
using App.Entities;
using App.Entities.Swagger;

namespace Tests.Services.Validations.Level3
{
    public class HttpVerbsValidate : Validation
    {
        [Theory]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET }, new HTTPVERBS[] { HTTPVERBS.GET }, 0)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET }, new HTTPVERBS[] { HTTPVERBS.POST }, 1)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.POST }, new HTTPVERBS[] { HTTPVERBS.PUT }, 1)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET }, new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, 1)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, 0)]
        [InlineData(new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST }, new HTTPVERBS[] { HTTPVERBS.GET, HTTPVERBS.POST, HTTPVERBS.PUT }, 1)]
        public void ReturnProperly(HTTPVERBS[] allowedVerbs, HTTPVERBS[] verbs, int expectedProblems)
        {
            var validator = new ValidatePathHttpVerbs(allowedVerbs);
            var endPoints = EndPoint.Create("path/", verbs);

            var output = ReturnProblems(validator, endPoints);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}