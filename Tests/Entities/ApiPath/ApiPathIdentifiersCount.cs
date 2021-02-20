using App.Entities;
using Xunit;

namespace Tests.Entities
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