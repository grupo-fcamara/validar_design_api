using App.Entities;
using Xunit;

namespace Tests.Entities
{
    public class ApiPathLevels
    {
        [Theory]
        [InlineData("/brands/{id}/models/{id}/versions", 3)]
        [InlineData("/brands/add/{id}/models/{id}/versions", 3)]
        [InlineData("usuarios/remover/{id}", 1)]
        public void ReturnProperly(string rawPath, int expectedLevel)
        {
            var path = new ApiPath(rawPath);
            Assert.Equal(expectedLevel, path.Levels);
        }
    }
}