using System.Linq;
using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level1 
{
    public class ValidateGetRoutesPerPath : IValidateGetRoutesPerPath
    {     
        public ValidationOutput Validate(Documentation documentation)
        {
            var output = new ValidationOutput();

            var groupedByFirstWord = documentation.Paths
                .GroupBy(path => new ApiPath(path.Key).Parts[0].ToString())
                .OrderBy(paths => paths.Count(path => path.Value.Get != null));

            foreach (var group in groupedByFirstWord)
            {
                int getsQuantity = group.Count(path => path.Value.Get != null);  

                if (getsQuantity > 2)
                    output.AddProblem($"{group.Key} has {getsQuantity} GET routes, the maximum is 2.");
            }
            return output;
        }
    }
}
