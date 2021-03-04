using App.Entities.Api;
using Xunit;

namespace Tests.Entities.Api
{
    public class ApiPathCtor
    {
        [Theory]
        [InlineData(""), InlineData(" "), InlineData(null)]
        public void ThrowExceptionIfInvalid(string rawPath)
        {
            Assert.Throws<System.Exception>(() => new ApiPath(rawPath));
        }
    }
}