using System.Linq;
using App.Entities.Output;
using App.Entities.Swagger;

namespace App.Services.Validations.Level2
{
    public class ValidatePathOperations : IValidatePathOperations
    {
        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            var paths = documentation.Paths;

            paths.Where(p => p.Operations.Length > 0)
                .ToList().ForEach(p => output.AddProblem($"Path {p.ToString()} has operations."));

            return output;
        }
    }
}