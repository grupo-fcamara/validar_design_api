using Xunit;
using System.Linq;
using App.Services.Validations.Level2;
using App.Entities;

namespace Tests.Services.Validations.Level2
{
    public class RoutesPatternValidate : Validation
    {
        [Theory]
        [InlineData(2, CasePattern.CAMEL, true,
            "users/{id}/pets/{id}", 
            "users/add/{id}", 
            "user/add/{id}", 
            "usuario/adicionar/{id}"
        )]
        public void ReturnProperly(int expectedProblems, CasePattern casePattern, bool plural, params string[] paths)
        {
            var output = ReturnProblems(new ValidateRoutesPattern(casePattern, plural), paths);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}