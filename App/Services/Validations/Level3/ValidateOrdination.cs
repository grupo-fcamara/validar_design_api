using System.Linq;
using App.Entities.Environment;
using App.Entities.Output;
using App.Entities.Swagger;
using Humanizer;

namespace App.Services.Validations.Level3
{
    public class ValidateOrdination : IValidateOrdination
    {
        readonly string[] possibleParams = new string[] { 
            "ascending", "descending", "asc", "desc", "orderBy",
            "crescente", "descrecente", "cres", "ordem", "ordemPor", "ordenarPor"
        };

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            var endPoints = documentation.EndPoints
                .Where(e => e.Verb == HttpVerbs.GET)
                .Where(e => !e.Path.Last.IsIdentifier);

            foreach (var endPoint in endPoints)
            {
                var parameters = endPoint.Parameters.Select(p => p.Name.Underscore());
                parameters.ToList().ForEach(System.Console.WriteLine);
                bool ordered = false;

                foreach (var param in possibleParams)
                {
                    if (parameters.Contains(param.Underscore()))
                        ordered = true;
                }

                if (!ordered)
                    output.AddProblem($"{endPoint.Path.ToString()} GET is not ordered.");
            }

            return output;
        }
    }
}