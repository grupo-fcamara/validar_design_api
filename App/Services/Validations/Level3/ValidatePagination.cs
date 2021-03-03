using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using Humanizer;

namespace App.Services.Validations.Level3
{
    public class ValidatePagination : IValidatePagination
    {
        readonly string[] possibleParams = new string[] { 
            "page", "per_page",
            "pagina", "por_pagina"
        };

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            var endPoints = documentation.EndPoints
                .Where(e => e.Verb == HttpVerbs.GET)
                .Where(e => !e.Path.Identifiers.Any());

            foreach (var endPoint in endPoints)
            {
                var parameters = endPoint.Parameters.Select(p => p.Name.Underscore());
                bool paginated = false;

                foreach (var param in possibleParams)
                {
                    if (parameters.Contains(param.Underscore()))
                        paginated = true;
                }

                if (!paginated)
                    output.AddProblem($"{endPoint.Path.ToString()} GET is not paginated.");
            }

            return output;
        }
    }
}