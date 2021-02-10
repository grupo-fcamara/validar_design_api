using System.Linq;
using App.Entities;
using Xunit;

namespace Tests
{
    public class ApiPathParts
    {
        [Theory]
        [InlineData("/brands/{id}/models/{id}/versions", "brands", "{id}", "models", "{id}", "versions")]
        public void ReturnProperly(string rawPath, params string[] expectedResult)
        {
            var path = new ApiPath(rawPath);
            Assert.Equal(expectedResult, path.Parts.Select(p => p.ToString()));
        }
    }
}