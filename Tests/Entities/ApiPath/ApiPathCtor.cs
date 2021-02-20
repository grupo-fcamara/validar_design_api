using App.Entities;
using Xunit;

namespace Tests.Entities
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