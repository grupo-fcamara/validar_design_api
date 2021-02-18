using System.Linq;
using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level2
{
    public class ValidatePathOperations : IValidatePathOperations
    {
        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();

            //Getting paths from document
            var rawPaths = documentation.GetPaths().Keys;
            var paths = rawPaths.Select(s => new ApiPath(s));

            paths.Where(p => p.Operations.Length > 0)
                .ToList().ForEach(p => output.AddProblem($"Path {p.ToString()} has operations."));

            return output;
        }
    }
}