using Xunit;
using System.Linq;
using App.Services.Validations.Level2;
using App.Entities.Environment;
using Tests.Services.Validations;

namespace Tests
{ 
    public class PathLanguageValidate : Validation
    {
        [Theory]
        [InlineData(1, Language.PORTUGUESE, "users/{id}", "usuarios/{id}")]
        public void ReturnProperly(int expectedProblems, Language language, params string[] paths) 
        {
            var output = ReturnProblems(new ValidatePathLanguage(language), paths);
            Assert.Equal(expectedProblems, output.Problems.Count()); 
        }
    }
} 