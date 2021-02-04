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

            var completePaths = documentation.Paths;
            var getFirstWord = completePaths.GroupBy(forEachPatch => new ApiPath(forEachPatch.Key).Pieces[0]);
            getFirstWord = getFirstWord.OrderBy(paths => paths.Count(path => path.Value.Get != null));

            foreach (var paths in getFirstWord)
            {
                int GetQuantityOfGetsForEachPath = paths.Count(path => path.Value.Get != null);  
                
                if(GetQuantityOfGetsForEachPath > 2) {
                    
                    output.AddProblem($"Multiple get routes in path: {paths.Key}, quantity: {GetQuantityOfGetsForEachPath}, max per path: 2");    
                }
            }
            return output;
        }
    }
}
