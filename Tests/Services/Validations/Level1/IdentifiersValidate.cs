using Xunit;
using System.Linq;
using App.Services.Validations.Level1;

namespace Tests.Services.Validations.Level1
{
    public class IdentifiersValidate : ValidatePaths
    {
        [Theory]
        [InlineData(1, "pets/{id}", "pets/{name}", "pets/{id}/users/{id}")]
        public void ReturnProperly(int expectedProblems, params string[] paths)
        {
            var output = ReturnProblems(new ValidateIdentifiers(), paths);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}