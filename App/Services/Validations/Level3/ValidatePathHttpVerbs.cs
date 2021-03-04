using App.Entities.Environment;
using App.Entities.Output;
using App.Entities.Swagger;
using System.Linq;

namespace App.Services.Validations.Level3
{
    public class ValidatePathHttpVerbs : Generic.IValidator
    {
        private HttpVerbs[] _allowedVerbs;

        public ValidatePathHttpVerbs(HttpVerbs[] allowedVerbs)
        {
            _allowedVerbs = allowedVerbs;
        }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();

            foreach (var group in documentation.EndPoints.GroupBy(e => e.Path.ToString()))
            {
                var verbsNotAllowed = group.Select(e => e.Verb).Except(_allowedVerbs);
                if (verbsNotAllowed.Any())
                    output.AddProblem($"Path {group.Key} has verbs that are not allowed: {string.Concat(verbsNotAllowed.Select(verb => verb + " "))}");
            }

            return output;
        }
    }
}