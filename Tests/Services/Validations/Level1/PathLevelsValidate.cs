using Xunit;
using System.Linq;
using App.Services.Validations.Level1;

namespace Tests.Services.Validations.Level1
{
    public class PathLevelsValidate : Validation
    {
        [Theory]
        [InlineData(2, 2, "users/{id}/pets/{id}", "countries/{id}/teams/{id}/players", "countries/{id}/teams/{id}/players/{id}")]
        public void ReturnProperly(int expectedProblems, int levelsLimit, params string[] paths)
        {
            var output = ReturnProblems(new ValidatePathLevels(levelsLimit), paths);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}