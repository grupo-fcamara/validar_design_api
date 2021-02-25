using App.Entities;
using Xunit;

namespace Tests.Entities
{
    public class ApiPathPartIsPlural
    {
        [Theory]
        #region Params

        #region Plural
        [InlineData("children", true)]
        [InlineData("dogs", true)]
        [InlineData("babies", true)]
        [InlineData("catches", true)]
        [InlineData("people", true)]
        [InlineData("series", true)]
        [InlineData("synopses", true)]
        [InlineData("wives", true)]
        [InlineData("halves", true)]
        [InlineData("women", true)]
        [InlineData("vertices", true)]
        [InlineData("matricies", true)]
        [InlineData("indicies", true)]
        [InlineData("axes", true)]
        [InlineData("media", true)]
        #endregion

        #region Singular
        [InlineData("child", false)]
        [InlineData("dog", false)]
        [InlineData("baby", false)]
        [InlineData("catch", false)]
        [InlineData("person", false)]
        [InlineData("synopsis", false)]
        [InlineData("wife", false)]
        [InlineData("half", false)]
        [InlineData("woman", false)]
        [InlineData("vertex", false)]
        [InlineData("matrix", false)]
        [InlineData("index", false)]
        [InlineData("axis", false)]
        [InlineData("medium", false)]
        #endregion

        #endregion
        public void Check(string text, bool expected)
        {
            var part = new ApiPathPart(text, null, null, 0);
            Assert.Equal(expected, part.IsPlural);
        }
    }
}