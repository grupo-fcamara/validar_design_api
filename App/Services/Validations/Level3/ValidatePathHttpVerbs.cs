using App.Entities;
using App.Entities.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace App.Services.Validations.Level3
{
    public class ValidatePathHttpVerbs : Generic.IValidator
    {
        private HTTPVERBS[] _allowedVerbs;

        public ValidatePathHttpVerbs(HTTPVERBS[] allowedVerbs)
        {
            _allowedVerbs = allowedVerbs;
        }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();

            foreach (var path in documentation.GetPaths())
            {
                var verbsNotAllowed = path.Value.GetVerbs().Except(_allowedVerbs);
                if (verbsNotAllowed.Count() > 0)
                    output.AddProblem($"Path {path.Key} has verbs that are not allowed: {string.Concat(verbsNotAllowed.Select(verb => verb + " "))}");
            }

            return output;
        }
    }
}