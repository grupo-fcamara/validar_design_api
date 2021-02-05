using App.Entities;
using Xunit;

namespace Tests
{
    public class ApiPathLevels
    {
        [Theory]
        [InlineData("/brands/{id}/models/{id}/versions", 3)]
        public void ReturnProperly(string rawPath, int expectedLevel)
        {
            var path = new ApiPath(rawPath);
            Assert.Equal(expectedLevel, path.Levels);
        }
    }
}