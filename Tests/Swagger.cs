using App.Entities.Swagger;
using Xunit;

namespace Tests
{
    public class SwaggerInfoIsValid
    {
        [Fact]
        public void IsValidWithRequiredProps()
        {
            var info = new SwaggerInfo();
            info.Title = "HAHAHA";
            info.Version = "2.0";

            Assert.True(info.IsValid);
        }

        [Theory]
        [InlineData("", "2.0")]
        [InlineData("HAHAH", "")]
        public void IsInvalidWithoutRequiredProps(string title, string version)
        {
            var info = new SwaggerInfo();
            info.Title = title;
            info.Version = version;

            Assert.False(info.IsValid);
        }
    }
}