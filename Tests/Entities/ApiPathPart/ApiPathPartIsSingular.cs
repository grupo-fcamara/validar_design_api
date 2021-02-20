using App.Entities;
using Xunit;

namespace Tests.Entities
{
    public class ApiPathPartIsSingular
    {
        [Theory]
        #region Params

        #region Singular
        [InlineData("child", true)]
        [InlineData("dog", true)]
        [InlineData("baby", true)]
        [InlineData("catch", true)]
        [InlineData("person", true)]
        [InlineData("synopsis", true)]
        [InlineData("wife", true)]
        [InlineData("half", true)]
        [InlineData("woman", true)]
        [InlineData("vertex", true)]
        [InlineData("matrix", true)]
        [InlineData("index", true)]
        [InlineData("axis", true)]
        [InlineData("medium", true)]
        #endregion

        #region Plural
        [InlineData("children", false)]
        [InlineData("dogs", false)]
        [InlineData("babies", false)]
        [InlineData("catches", false)]
        [InlineData("people", false)]
        [InlineData("synopses", false)]
        [InlineData("wives", false)]
        [InlineData("halves", false)]
        [InlineData("women", false)]
        [InlineData("vertices", false)]
        [InlineData("matricies", false)]
        [InlineData("indicies", false)]
        [InlineData("axes", false)]
        [InlineData("media", false)]
        #endregion

        #endregion
        public void Check(string text, bool expected)
        {
            var part = new ApiPathPart(text, null, null, 0);
            Assert.Equal(part.IsSingular, expected);
        }
    }
}