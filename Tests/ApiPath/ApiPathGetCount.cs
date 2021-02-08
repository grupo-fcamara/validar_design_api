using App.Services.Validations.Level1;
using Xunit;

namespace Tests
{ 
    public class ApiPathGetCount
    {
        [Fact]
        public void Verify_Quantity_Of_Get_Routes_Per_Path() 
        {   
            var expectedBoolean = true;
            var gets = new ValidateGetRoutesPerPath();
            var result = gets.IsValid;
            Assert.Equal(expectedBoolean, result);
        }
    }
}