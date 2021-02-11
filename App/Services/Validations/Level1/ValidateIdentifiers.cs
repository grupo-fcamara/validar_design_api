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

            //Checking if there are multiple identifiers at the same resource
            var groupedIdentifiers = identifiers.GroupBy(GetResourceFromIdentifier);
            foreach (var group in groupedIdentifiers)
            {
                var distinct = group.Distinct(p => p.ToString());
                var names = distinct.Select(i => i.ToString());

                if (names.Count() > 1)
                {
                    string ids = "";
                    names.ToList().ForEach(id => ids += $"{id} ");
                    output.AddProblem($"Multiple identifiers in {group.Key}: {ids}");
                }                   
            }

            return output;
        }

        private string GetResourceFromIdentifier(ApiPathPart part)
        {
            if (part.Parent == null)
                return null;
            if (part.Parent.IsResource)
                return part.Parent.ToString();
            else
                return GetResourceFromIdentifier(part.Parent);
        }
    }
}