using System.Collections.Generic;
using System.Linq;

using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level1
{
    public class ValidateIdentifiers : IValidateIdentifiers
    {
        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();

            //Getting paths from document
            var rawPaths = documentation.GetPaths().Keys;
            var paths = rawPaths.Select(s => new ApiPath(s));

            //Validating
            output.Concat(ValidateLevel(paths, 0));
            return output;
        }

        private ValidationOutput ValidateLevel(IEnumerable<ApiPath> paths, int level, string parent = "")
        {
            var output = new ValidationOutput();

            paths = paths.Where(path => path.Parts.Length > level);
            var grouped = paths.GroupBy(p => p.Parts[level].ToString());

            var identifiers = paths.Select(path => path.Parts[level]).Where(part => part.IsIdentifier);
            
            var distinct = identifiers.Distinct(i => i.ToString());
            if (distinct.Count() > 1)
                output.AddProblem($"Multiple identifiers in {parent}: {string.Concat(distinct.Select(d => d + " "))}");

            foreach (var group in grouped)
            {
                output.Concat(ValidateLevel(group, level + 1, group.Key));
            }

            return output;
        }
    }
}