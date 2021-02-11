using App.Services;
using Xunit;

namespace Tests
{
    public class OperationCheckerServiceIsOperation
    {
        [Theory]
        [InlineData("find", true)]
        [InlineData("finder", false)]
        [InlineData("findById", true)]
        [InlineData("adicionarUsuario", true)]
        public void ReturnProperly(string text, bool expected)
        {
            Assert.Equal(OperationCheckerService.IsOperation(text), expected);
        }
    }
}