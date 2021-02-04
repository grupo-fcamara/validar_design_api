using App.Entities;
using Xunit;

namespace Tests
{
    public class ApiPathIdentifiersCount
    {
        [Theory]
        [InlineData("/brands/{id}/models/{id}/versions", 2)]
        public void ReturnProperly(string rawPath, int expectedQuantity)
        {
            var path = new ApiPath(rawPath);
            Assert.Equal(expectedQuantity, path.IdentifiersCount);
        }
    }
}