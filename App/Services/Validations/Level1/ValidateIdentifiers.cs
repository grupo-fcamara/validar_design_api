using System.Collections.Generic;
using System.Linq;

using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level1
{
    public class ValidateIdentifiers : IValidateIdentifiers
    {
        struct Identifier
        {
            public string Parent { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }

            public Identifier(string parent, string name, string path)
            {
                Parent = parent;
                Name = name;
                Path = path;
            }
        }

        public ValidationOutput Validate(Documentation documentation)
        {
            var output = new ValidationOutput();

            //Getting paths from document
            var rawPaths = documentation.Paths.Keys;
            var paths = rawPaths.Select(s => new ApiPath(s));

            //Getting all identifiers
            var identifiers = new List<Identifier>();
            foreach (var path in paths)
            {
                path.Identifiers.ToList().ForEach(
                    i => identifiers.Add(new Identifier(path.Pieces[i.Key - 1], i.Value, path.ToString())
                ));  
            }

            //Checking if there are multiple identifiers
            var groupedIdentifiers = identifiers.GroupBy(i => i.Parent);
            foreach (var group in groupedIdentifiers)
            {
                var names = group.Select(i => i.Name).Distinct();
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