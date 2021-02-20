using System.Linq;
using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level1 
{
    public class ValidateGetRoutesPerPath : IValidateGetRoutesPerPath
    {     
        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();

            var groupedByFirstWord = documentation.EndPoints
                .GroupBy(endPoint => endPoint.Path.Parts[0].ToString())
                .OrderBy(group => group.Count(endPoint => endPoint.Verb == HTTPVERBS.GET));

            foreach (var group in groupedByFirstWord)
            {
                int getsQuantity = group.Count(endPoint => endPoint.Verb == HTTPVERBS.GET);  

                if (getsQuantity > 2)
                    output.AddProblem($"{group.Key} has {getsQuantity} GET routes, the maximum is 2.");
            }
            return output;
        }
    }
}
