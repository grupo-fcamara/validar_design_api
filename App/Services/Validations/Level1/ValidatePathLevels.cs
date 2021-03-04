using System.Linq;
using App.Entities.Output;
using App.Entities.Swagger;

namespace App.Services.Validations.Level1
{
    public class ValidatePathLevels : IValidatePathLevels
    {
        int limit;

        //Default = 2
        public ValidatePathLevels(int limit) { this.limit = limit; }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            var paths = documentation.Paths;

            foreach (var path in paths.Where(p => p.Levels > limit))
            {
                output.AddProblem(
                    $"Path {path.ToString()} has {path.Levels} levels, the maximum is {limit}."
                );
            }

            return output;
        }
    }
}