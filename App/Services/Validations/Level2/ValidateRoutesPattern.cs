using System.Collections.Generic;
using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using Humanizer;

namespace App.Services.Validations.Level2
{
    public class ValidateRoutesPattern :  IValidateRoutesPattern
    {
        CASE casePattern;
        bool plural;

        public ValidateRoutesPattern(CASE casePattern, bool plural)
        {
            this.casePattern = casePattern;
            this.plural = plural;
        }

        public ValidationOutput Validate(Documentation documentation)
        {
            var output = new ValidationOutput();

            //Getting paths from document
            var rawPaths = documentation.Paths.Keys;
            var paths = rawPaths.Select(s => new ApiPath(s));

            foreach (var path in paths)
            {
                if (path.Parts.Count(p => !p.IsRespectingCase(casePattern)) > 0)
                    output.AddProblem($"Path {path.ToString()} is not respecting the {casePattern.ToString().ToLower()} case pattern.");
            }

            return output;
        }
    }
}