using App.Entities;
using Xunit;

namespace Tests.Entities
{
    public class ApiPathPartIsRespectingCase
    {
        [Theory]
        #region Params
        [InlineData("camelCase", CasePattern.CAMEL)]
        [InlineData("snake_case", CasePattern.SNAKE)]
        [InlineData("spinal-case", CasePattern.SPINAL)]
        [InlineData("PascalCase", CasePattern.CAMEL)]
        [InlineData("simple", CasePattern.CAMEL)]
        #endregion
        public void Accept(string text, CasePattern casePattern)
        {
            var part = new ApiPathPart(text, null, null, 0);
            Assert.True(part.IsRespectingCase(casePattern));
        }

        [Theory]
        #region Params
        [InlineData("camelCase", CasePattern.SNAKE)]
        [InlineData("camelCase", CasePattern.SPINAL)]
        [InlineData("snake_case", CasePattern.CAMEL)]
        [InlineData("snake_case", CasePattern.SPINAL)]
        [InlineData("spinal-case", CasePattern.CAMEL)]
        [InlineData("spinal-case", CasePattern.SNAKE)]
        [InlineData("wrong_Case", CasePattern.CAMEL)]
        [InlineData("wrong_Case", CasePattern.SNAKE)]
        [InlineData("wrong_Case", CasePattern.SPINAL)]
        [InlineData("wrong-Case", CasePattern.CAMEL)]
        [InlineData("wrong-Case", CasePattern.SNAKE)]
        [InlineData("wrong-Case", CasePattern.SPINAL)]
        [InlineData("wtf_-wtf", CasePattern.SNAKE)]
        [InlineData("wtf_-wtf", CasePattern.SPINAL)]
        #endregion
        public void Reject(string text, CasePattern casePattern)
        {
            var part = new ApiPathPart(text, null, null, 0);
            Assert.False(part.IsRespectingCase(casePattern));
        }
    }
}