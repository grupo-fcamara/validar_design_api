using App.Entities;
using Xunit;

namespace Tests
{
    public class ApiPathPartIsRespectingCase
    {
        [Theory]
        #region Params
        [InlineData("camelCase", CASE.CAMEL)]
        [InlineData("snake_case", CASE.SNAKE)]
        [InlineData("spinal-case", CASE.SPINAL)]
        [InlineData("PascalCase", CASE.CAMEL)]
        [InlineData("simple", CASE.CAMEL)]
        #endregion
        public void Accept(string text, CASE casePattern)
        {
            var part = new ApiPathPart(text, null, null, 0);
            Assert.True(part.IsRespectingCase(casePattern));
        }

        [Theory]
        #region Params
        [InlineData("camelCase", CASE.SNAKE)]
        [InlineData("camelCase", CASE.SPINAL)]
        [InlineData("snake_case", CASE.CAMEL)]
        [InlineData("snake_case", CASE.SPINAL)]
        [InlineData("spinal-case", CASE.CAMEL)]
        [InlineData("spinal-case", CASE.SNAKE)]
        [InlineData("wrong_Case", CASE.CAMEL)]
        [InlineData("wrong_Case", CASE.SNAKE)]
        [InlineData("wrong_Case", CASE.SPINAL)]
        [InlineData("wrong-Case", CASE.CAMEL)]
        [InlineData("wrong-Case", CASE.SNAKE)]
        [InlineData("wrong-Case", CASE.SPINAL)]
        [InlineData("wtf_-wtf", CASE.SNAKE)]
        [InlineData("wtf_-wtf", CASE.SPINAL)]
        #endregion
        public void Reject(string text, CASE casePattern)
        {
            var part = new ApiPathPart(text, null, null, 0);
            Assert.False(part.IsRespectingCase(casePattern));
        }
    }
}