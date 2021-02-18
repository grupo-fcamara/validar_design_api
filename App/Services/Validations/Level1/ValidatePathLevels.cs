using System.Collections.Generic;
using System.Linq;

using App.Entities;
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

            //Getting paths from document
            var rawPaths = documentation.GetPaths().Keys;
            var paths = rawPaths.Select(s => new ApiPath(s));

            //Validating
            paths.Where(p => p.Levels > limit)
                .ToList().ForEach(p => output.AddProblem(
                    $"Path {p.ToString()} has {p.Levels} levels, the maximum is {limit}."
                )
            );

            return output;
        }
    }
}