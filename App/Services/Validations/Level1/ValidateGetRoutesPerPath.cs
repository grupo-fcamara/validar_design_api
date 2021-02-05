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
                .GroupBy(path => new ApiPath(path.Key).Pieces[0])
                .OrderBy(paths => paths.Count(path => path.Value.Get != null));

            foreach (var paths in groupedByFirstWord)
            {
                int GetQuantityOfGetsForEachPath = paths.Count(path => path.Value.Get != null);  
                
                if(GetQuantityOfGetsForEachPath > 2) {                      
                    output.AddProblem($"{paths.Key} has {GetQuantityOfGetsForEachPath} GET routes, the maximum is 2.");    
                }
            }
            return output;
        }
    }
}
