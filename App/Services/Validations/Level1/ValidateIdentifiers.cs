using System.Collections.Generic;
using System.Linq;

using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level1
{
    public class ValidateIdentifiers : IValidateIdentifiers
    {
        public ValidationOutput Validate(Documentation documentation)
        {
            var output = new ValidationOutput();

            //Getting paths from document
            var rawPaths = documentation.Paths.Keys;
            var paths = rawPaths.Select(s => new ApiPath(s));

            //Getting all identifiers
            var identifiers = new List<ApiPathPart>();
            paths.ToList().ForEach(path => identifiers.AddRange(path.Identifiers));

            //Checking if there are multiple identifiers
            var groupedIdentifiers = identifiers.GroupBy(i => i.Parent);
            foreach (var group in groupedIdentifiers)
            {
                var names = group.Select(i => i.ToString()).Distinct();
                if (names.Count() > 1)
                {
                    string ids = "";
                    names.ToList().ForEach(id => ids += $"{id} ");
                    output.AddProblem($"Multiple identifiers in {group.Key}: {ids}");
                }                   
            }

            return output;
        }
    }
}