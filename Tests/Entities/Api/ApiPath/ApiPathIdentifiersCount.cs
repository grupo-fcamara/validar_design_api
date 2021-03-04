using App.Entities.Api;
using Xunit;

namespace Tests.Entities.Api
{
    public class ApiPathIdentifiersCount
    {
        [Theory]
        [InlineData("/brands/{id}/models/{id}/versions", 2)]
        public void ReturnProperly(string rawPath, int expectedQuantity)
        {
            var path = new ApiPath(rawPath);
            Assert.Equal(expectedQuantity, path.Identifiers.Length);
        }
    }
}