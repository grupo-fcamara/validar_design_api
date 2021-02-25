using Xunit;
using System.Linq;
using App.Services.Validations.Level2;

namespace Tests.Services.Validations.Level2
{
    public class PathOperationsValidate : Validation
    {
        [Theory]
        [InlineData(2, 
            "users/{id}/pets/{id}", 
            "users/add/{id}", 
            "usuarios/adicionar/{id}"
        )]
        public void ReturnProperly(int expectedProblems, params string[] paths)
        {
            var output = ReturnProblems(new ValidatePathOperations(), paths);
            Assert.Equal(expectedProblems, output.Problems.Count());
        }
    }
}